using System;
using System.Text;
using System.Collections.Generic;

using Xen;
using Xen.Camera;
using Xen.Graphics;
using Xen.Ex.Graphics;
using Xen.Ex.Graphics2D;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient
{
    //The following class stores constants describing the rendering state of the scene.
    //The user interface will display all float/boolean properties of this class for editing.
    class RenderConfiguration
    {
        private readonly Game game;
        public RenderConfiguration(Game tutorial) { this.game = tutorial; }

        //configuration flags:

        private bool showAllControls;

        private bool showEncodedRgbmRenderTarget;
        private bool pauseModelAnimation;
        private bool pauseModelRotation;
        private bool showBloomRenderTarget;

        private float ambientSphericalHarmonicScale;
        private float diffuseLightingScale;
        private float specularLightingScale;
        private float skinLightScatteringScale;

        private float albedoTextureScale;
        private float ambientOcclusionTextureScale;
        private float shadowMapTermScale;

        //brightness controls

        //Lens Exposure constant (global scale for lighting)
        public float TargetLensExposure;	//desired value
        public float LensExposure;			//actual value

        //Enables / Disables if tonemapping functions are applied to the output image to smooth exposure.
        //Note, this is unrelated to the 'LensExposure' float, which controls global light level.
        private bool useExposureTonemapping, useFilmApproxToneMapping, useInverseOneTonemapping;
        private bool useGammaCorrection;


        //Properties displayed on the UI:

        //This attribute indicates there should be a small gap displayed between menu items
        internal class GapAttribute : Attribute { }

        [Gap]
        public bool ShowOptions { get { return showAllControls; } set { showAllControls = value; } }

        //debug helpers
        public bool ShowBloomRenderTarget { get { return showBloomRenderTarget; } set { showBloomRenderTarget = value; } }
        [Gap]
        public bool ShowEncodedRgbmRenderTarget { get { return showEncodedRgbmRenderTarget; } set { showEncodedRgbmRenderTarget = value; } }
        public bool PauseModelRotation { get { return pauseModelRotation; } set { pauseModelRotation = value; } }
        [Gap]
        public bool PauseModelAnimation { get { return pauseModelAnimation; } set { pauseModelAnimation = value; } }

        //rendering globals
        public float AmbientSphericalHarmonicScale { get { return ambientSphericalHarmonicScale; } set { ambientSphericalHarmonicScale = value; } }
        public float DiffuseLightingScale { get { return diffuseLightingScale; } set { diffuseLightingScale = value; } }
        public float SpecularLightingScale { get { return specularLightingScale; } set { specularLightingScale = value; } }

        //Term for fake scattering of light in the skin
        public float SkinLightScatteringScale { get { return skinLightScatteringScale; } set { skinLightScatteringScale = value; } }

        //set to 1 to disable the effect, zero to enable
        public float AlbedoTextureScale { get { return albedoTextureScale; } set { albedoTextureScale = value; } }
        public float AmbientOcclusionTextureScale { get { return ambientOcclusionTextureScale; } set { ambientOcclusionTextureScale = value; } }
        [Gap]
        public float ShadowMapTermScale { get { return shadowMapTermScale; } set { shadowMapTermScale = value; } }

        //Enables / Disables gamma correction in the output.
        [Gap]
        public bool UseGammaCorrection { get { return useGammaCorrection; } set { useGammaCorrection = value; } }

        public bool NoTonemapping { get { return useExposureTonemapping == useFilmApproxToneMapping == useInverseOneTonemapping == false; } set { useExposureTonemapping = false; useFilmApproxToneMapping = false; useInverseOneTonemapping = false; } }
        public bool UseInverseOneTonemapping { get { return useInverseOneTonemapping; } set { useExposureTonemapping = false; useFilmApproxToneMapping = false; useInverseOneTonemapping = true; } }
        public bool UseFilmApproximationTonemapping { get { return useFilmApproxToneMapping; } set { useExposureTonemapping = false; useFilmApproxToneMapping = true; useInverseOneTonemapping = false; } }
        [Gap]
        public bool UseExposureTonemapping { get { return useExposureTonemapping; } set { useExposureTonemapping = true; useFilmApproxToneMapping = false; useInverseOneTonemapping = false; } }

        //callbacks into the tutorial
        public bool HalfLensExposure { set { game.ScaleExposure(0.5f); } }
        public bool DoubleLensExposure { set { game.ScaleExposure(2); } }
        public bool ChangeScene { set { game.ChangeScene(); } }
    }

    //Scene specific configuration
    class SceneConfiguration
    {
        public SceneConfiguration Clone() { return (SceneConfiguration)this.MemberwiseClone(); }

        //camera
        public Vector3 DefaultCamPos, DefaultCamViewPos;

        //sun rendering params
        public Vector3 SunDirection;
        public Vector3 SunColour;
        public float SunIntensity;
        public float SunSpecularPower;
        public float SunSpecularIntensity;

        //Term for fake scattering of light in the skin
        public Vector3 SkinLightScattering;

        //The RGBM encoding scale used by background images
        public float RgbmImageScale;

        //The RGBM rendering scale (dynamic range) for scene rendering
        //This value represents the maximum brightness that can be encoded in the output.
        //Setting this value too high will produce noticable banding, setting it too low 
        //will clamp bright values.
        public float RgbmRenderScale;

        //Threshold for bloom rendering (in exposure adjusted gamma space)
        public float BloomThreshold;

        //Bloom output scale
        public float BloomScale;

        //Background texture and spherical harmonic
        public RgbmCubeMap BackgroundScene;

        //brightness controls

        //Lens Exposure constant (global scale for lighting)
        public float DefaultLensExposure;
    }



    //This class allows editing and changing rendering config params in the RenderConfiguration class.
    //This is done through reflection, which is usually far too slow for realtime use, however here it's very infrequent.
    class RenderConfigEditor : IDraw, IUpdate, IContentOwner
    {
        private static readonly Dictionary<string, System.Reflection.PropertyInfo> ConfigProperties;

        //construct a dictionary storing all the editable values in the render config.
        static RenderConfigEditor()
        {
            ConfigProperties = new Dictionary<string, System.Reflection.PropertyInfo>();

            //This will extract all the public properties from the render config class
            System.Reflection.PropertyInfo[] properties = typeof(RenderConfiguration).GetProperties();

            foreach (System.Reflection.PropertyInfo property in properties)
            {
                //collect the float/boolean types in the class
                if (property.CanWrite &&
                    ((property.PropertyType == typeof(float) && property.CanRead) || property.PropertyType == typeof(bool)))
                {
                    //add a space before all capital letters
                    List<char> characters = new List<char>(property.Name.ToCharArray());
                    for (int i = 1; i < characters.Count; i++)
                    {
                        if (char.IsUpper(characters[i]))
                        {
                            characters.Insert(i, ' ');
                            i++;
                        }
                    }
                    string name = new string(characters.ToArray());
                    //remove 'Scale' if it's there
                    name = name.Replace(" Scale", "");

                    ConfigProperties.Add(name, property);
                }
            }
        }

        private RenderConfiguration instance;
        private int editSelection;
        private readonly object[] emptyArray = new object[0];

        //Store the text elements displaying the state of each render config state.
        private readonly List<TextElement> stateText;
        private readonly List<bool> visibleElementGap; //display a gap before the element?
        private readonly SolidColourElement backgroundContainer;
        private TextElement helpText;


        public RenderConfigEditor(ContentRegister content)
        {
            //setup the text elements
            this.stateText = new List<TextElement>();
            this.visibleElementGap = new List<bool>();

#if XBOX360
			this.helpText = new TextElement("Use 'A' and the DPad to interact with the menu");
#else
            this.helpText = new TextElement("Use the Arrow Keys and 'Enter' to interact with the menu");
#endif
            this.helpText.HorizontalAlignment = HorizontalAlignment.Left;
            this.helpText.VerticalAlignment = VerticalAlignment.Bottom;
            this.helpText.Colour = Color.Black;

            this.backgroundContainer = new SolidColourElement(new Color(0, 0, 0, 200), new Vector2(0, 0));
            this.backgroundContainer.AlphaBlendState = AlphaBlendState.Alpha;

            foreach (string name in ConfigProperties.Keys)
            {
                //create the text
                TextElement text = new TextElement();

                //if it's a ediable value, then put a '[X]' infront
                if (ConfigProperties[name].CanRead)
                    text.Text.SetText("[ ] " + name);
                else
                    text.Text.SetText(name);

                text.VerticalAlignment = VerticalAlignment.Bottom;
                this.stateText.Add(text);

                bool gap = false;
                if (ConfigProperties[name].GetCustomAttributes(typeof(RenderConfiguration.GapAttribute), false).Length > 0)
                    gap = true;

                this.visibleElementGap.Add(gap);
            }

            //select top instance
            this.editSelection = this.stateText.Count - 1;

            //sizes of the elements are setup in LoadContent()
            content.Add(this);
        }

        public void Draw(DrawState state)
        {
            SetupTextSize(state.Application);

            //draw the background first
            backgroundContainer.Draw(state);

            for (int i = 0; i < this.stateText.Count; i++)
                this.stateText[i].Draw(state);

            if (this.helpText != null)
                this.helpText.Draw(state);
        }

        bool ICullable.CullTest(ICuller culler)
        {
            return true;
        }


        //Helper method:
        //returns true if the given button is down, or repeats ten times per second when held down for more than quater of a second
        private bool ButtonIsDown(Xen.Input.State.Button button, UpdateState state)
        {
            float repeatEvery = 0.1f;
            float repeatAfter = 0.15f;

            if (button.IsDown && button.DownDuration >= repeatAfter)
            {
                float time = button.DownDuration / repeatEvery;
                float frac = time - (float)Math.Floor(time);
                return frac * repeatEvery < state.DeltaTimeSeconds ||
                    repeatAfter + state.DeltaTimeSeconds > button.DownDuration;
            }
            else
                return button.OnReleased && button.DownDuration < repeatAfter;
        }



        public UpdateFrequency Update(UpdateState state)
        {
            //read DPad up/down to change selection
            if (instance != null && instance.ShowOptions)
            {
                if (ButtonIsDown(state.PlayerInput[0].InputState.Buttons.DpadDown, state))
                    this.editSelection--;
                if (ButtonIsDown(state.PlayerInput[0].InputState.Buttons.DpadUp, state))
                    this.editSelection++;

                //wrap the selection index to a valid range
                if (this.editSelection < 0) this.editSelection += this.stateText.Count;
                if (this.editSelection >= this.stateText.Count) this.editSelection -= this.stateText.Count;
            }

            //user has made a selection?
            if (state.PlayerInput[0].InputState.Buttons.A.OnReleased)
            {
                //toggle the selected state
                ToggleValue(this.EditSelection);

                //as soon as a button is pressed, remove the help text.
                this.helpText = null;
            }

            //update the UI colours to match the current selection
            UpdateUIColours();

            return UpdateFrequency.FullUpdate60hz;
        }

        //public setter for changing instance
        public RenderConfiguration Instance
        {
            set
            {
                if (this.instance != value)
                {
                    this.instance = value;
                    UpdateUIValues();
                }
            }
        }

        private int EditSelection
        {
            get
            {
                int selection = editSelection;
                if (instance != null && instance.ShowOptions == false)
                    selection = 0;
                return selection;
            }
        }

        private void UpdateUIColours()
        {
            Color highlight = new Color(0, 255, 0, 255);
            Color fade = new Color(255, 255, 255, 128);

            for (int i = 0; i < this.stateText.Count; i++)
                stateText[i].Colour = i == EditSelection ? highlight : fade;
        }

        private void UpdateUIValues()
        {
            //updates the UI to match the instance state
            int index = 0;

            foreach (System.Reflection.PropertyInfo property in ConfigProperties.Values)
            {
                if (property.CanRead)
                {
                    //this is rather slow and nasty... As reflection is being used
                    object target = property.GetValue(instance, emptyArray);
                    bool value;

                    //value is either a float or boolean
                    if (target is bool)
                        value = (bool)target;
                    else
                        value = (float)target == 1;

                    //set the first character to X or blank, to fill the '[ ]'
                    this.stateText[index].Text.SetCharacter(value ? 'X' : ' ', 1);
                }

                index++;
            }
        }

        private void ToggleValue(int selectedIndex)
        {
            //Toggle the value in the config, based on user input
            int index = 0;

            foreach (System.Reflection.PropertyInfo property in ConfigProperties.Values)
            {
                if (index == selectedIndex)
                {
                    if (property.CanRead)
                    {
                        //get the current value
                        object target = property.GetValue(instance, emptyArray);

                        //toggle the value
                        if (target is bool)
                            property.SetValue(instance, !(bool)target, emptyArray);
                        else
                            property.SetValue(instance, 1 - (float)target, emptyArray);
                    }
                    else //only boolean properties with just a setter can be added without a getter
                        property.SetValue(instance, true, emptyArray); //send a 'true'.
                }
                index++;
            }

            //update the UI
            UpdateUIValues();
        }


        void IContentOwner.LoadContent(ContentState state)
        {
            //setup the font
            SpriteFont font = state.Load<SpriteFont>(@"CourierNew");

            if (this.helpText != null)
                this.helpText.Font = state.Load<SpriteFont>(@"Arial");

            for (int i = 0; i < stateText.Count; i++)
                stateText[i].Font = font;

            Application application = state.Application;

            SetupTextSize(application);
        }

        //align the text elements
        private void SetupTextSize(Application application)
        {
            //setup the item sizes
            Vector2 windowSize = new Vector2(application.WindowWidth, application.WindowHeight);

            float height = 0;
            float maxWidth = 0;

            float offset = 4;
            //work out an approximate offset for title safety
            float titleOffsetX = Math.Max(16, application.WindowWidth - application.TitleSafeArea.Width) / 2;
            float titleOffsetY = Math.Max(16, application.WindowHeight - application.TitleSafeArea.Height) / 2;

            if (this.instance != null && this.instance.ShowOptions == false)
            {
                for (int i = 0; i < stateText.Count; i++)
                    stateText[i].Visible = false;
            }

            //space the text out vertically
            for (int i = 0; i < stateText.Count; i++)
            {
                Vector2 size = stateText[i].Font.MeasureString((StringBuilder)stateText[i].Text);
                maxWidth = Math.Max(maxWidth, size.X);

                height += stateText[i].Font.LineSpacing + 2;
                stateText[i].Position = new Vector2(offset + titleOffsetX, height + offset + titleOffsetY);

                if (this.instance != null)
                {
                    stateText[i].Visible = true;
                    if (this.instance.ShowOptions == false)
                        break;
                }

                if (visibleElementGap[i])
                    height += 8;
            }

            //set the background to fill in
            this.backgroundContainer.Size = new Vector2(maxWidth + offset * 2, height + offset * 2);
            this.backgroundContainer.Position = new Vector2(titleOffsetX, titleOffsetY);

            if (this.helpText != null)
                this.helpText.Position = this.backgroundContainer.Position + new Vector2(this.backgroundContainer.Size.X + 16, this.helpText.Font.LineSpacing);
        }
    }
}
