using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Forms9Patch
{
    [ContentProperty("Segments")]
    public class TargetedMenu : BubblePopup
    {
        static Color DefaultBackgroundColor = Color.FromRgb(51, 51, 51);
        static Color DefaultTextColor = Color.White;
        static Color DefaultSeparatorColor = DefaultTextColor;
        static double DefaultSeparatorWidth = 1.0;

        #region Properties

        #region Segments property
        readonly ObservableCollection<Segment> _segments = new ObservableCollection<Segment>();
        /// <summary>
        /// The container for the Segmented Control's buttons.
        /// </summary>
        /// <value>The buttons.</value>
        public IList<Segment> Segments
        {
            get { return _segments; }
            set
            {
                _segments.Clear();
                if (value != null)
                {
                    foreach (var segment in value)
                        _segments.Add(segment);
                }
            }
        }
        #endregion Segments property

        #region FontSize property
        /// <summary>
        /// backing store for FontSize property
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double), typeof(TargetedMenu), -1.0);
        /// <summary>
        /// Gets/Sets the FontSize property
        /// </summary>
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        #endregion FontSize property

        #region TextColor property
        /// <summary>
        /// backing store for FontColor property
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Xamarin.Forms.Color), typeof(TargetedMenu), DefaultTextColor);
        /// <summary>
        /// Gets/Sets the FontColor property
        /// </summary>
        public Xamarin.Forms.Color TextColor
        {
            get { return (Xamarin.Forms.Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        #endregion TextColor property

        #region SeparatorColor property
        /// <summary>
        /// backing store for SeparatorColor property
        /// </summary>
        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create("SeparatorColor", typeof(Color), typeof(TargetedMenu), DefaultSeparatorColor);
        /// <summary>
        /// Gets/Sets the SeparatorColor property
        /// </summary>
        public Color SeparatorColor
        {
            get { return (Color)GetValue(SeparatorColorProperty); }
            set { SetValue(SeparatorColorProperty, value); }
        }
        #endregion SeparatorColor property

        #region SeparatorWidth property
        /// <summary>
        /// backing store for SeparatorWidth property
        /// </summary>
        public static readonly BindableProperty SeparatorWidthProperty = BindableProperty.Create("SeparatorWidth", typeof(double), typeof(TargetedMenu), DefaultSeparatorWidth);
        /// <summary>
        /// Gets/Sets the SeparatorWidth property
        /// </summary>
        public double SeparatorWidth
        {
            get { return (double)GetValue(SeparatorWidthProperty); }
            set { SetValue(SeparatorWidthProperty, value); }
        }
        #endregion SeparatorWidth property

        #region HapticEffect property
        /// <summary>
        /// backing store for HapticEffect property
        /// </summary>
        public static readonly BindableProperty HapticEffectProperty = BindableProperty.Create("HapticEffect", typeof(HapticEffect), typeof(TargetedMenu), default(HapticEffect));
        /// <summary>
        /// Gets/Sets the HapticEffect property
        /// </summary>
        public HapticEffect HapticEffect
        {
            get { return (HapticEffect)GetValue(HapticEffectProperty); }
            set { SetValue(HapticEffectProperty, value); }
        }
        #endregion HapticEffect property

        #region HapticMode property
        /// <summary>
        /// backing store for HapticMode property
        /// </summary>
        public static readonly BindableProperty HapticModeProperty = BindableProperty.Create("HapticMode", typeof(KeyClicks), typeof(TargetedMenu), default(KeyClicks));
        /// <summary>
        /// Gets/Sets the HapticMode property
        /// </summary>
        public KeyClicks HapticMode
        {
            get { return (KeyClicks)GetValue(HapticModeProperty); }
            set { SetValue(HapticModeProperty, value); }
        }
        #endregion HapticMode property

        #endregion


        #region Fields


        //readonly List<Button> _buttons;
        //readonly List<Size> _buttonSizes;
        int _currentPage;
        readonly Button _leftArrowButton = new Button
        {
            FontFamily = "Forms9Patch.Resources.Fonts.MaterialIcons.ttf",
            HtmlText = "&#xE314;",
            Padding = new Thickness(4, 0, 4, 4),
            TextColor = DefaultTextColor,
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Start,
            BackgroundColor = DefaultBackgroundColor,
            Lines=1,
            AutoFit = AutoFit.None,
        };
        readonly Button _rightArrowButton = new Button
        {
            FontFamily = "Forms9Patch.Resources.Fonts.MaterialIcons.ttf",
            HtmlText = "&#xE315;",
            Padding = new Thickness(4, 0, 4, 4),
            TextColor = DefaultTextColor,
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.End,
            BackgroundColor = DefaultBackgroundColor,
            Lines = 1,
            AutoFit = AutoFit.None,
        };
        Forms9Patch.StackLayout _stackLayout = new Forms9Patch.StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Spacing = DefaultSeparatorWidth,
            Padding = 0,
            Margin = 0,
            BackgroundColor = Color.White,
            OutlineColor = DefaultBackgroundColor,
            OutlineWidth = 1,
            OutlineRadius = 4,
        };
        #endregion


        #region Constructor / Factory
        /// <summary>
        /// Instantiates, populates and presents a TargetedMenu
        /// </summary>
        /// <param name="target">VisualElement to target</param>
        /// <param name="htmlTexts">List of text for menu items (with optional HTML markup)</param>
        /// <returns></returns>
        public static TargetedMenu Create(VisualElement target, List<string> htmlTexts=null)
        {
            var targetedMenu = new TargetedMenu(target);
            if (htmlTexts!=null)
            {
                foreach (var htmlText in htmlTexts)
                    targetedMenu.Segments.Add(new Segment
                    {
                        HtmlText = htmlText
                    });
            }
            targetedMenu.IsVisible = true;
            return targetedMenu;
        }

        /// <summary>
        /// Constructor for TargetedToast
        /// </summary>
        /// <param name="target"></param>
        public TargetedMenu(VisualElement target) : base(target)
        {
            PointerDirection = PointerDirection.Any;
            PreferredPointerDirection = PointerDirection.Down;
            BackgroundColor = DefaultBackgroundColor;
            HasShadow = false;
            Padding = 0;
            Margin = 10;
            OutlineRadius = 4;
            PageOverlayColor = Color.Black.WithAlpha(0.1);
            _segments.CollectionChanged += OnSegmentsCollectionChanged;
            SizeChanged += OnSizeChanged;
            Content = _stackLayout;
        }
        #endregion

        #region PropertyChange handlers
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == BackgroundColorProperty.PropertyName)
            {
                foreach (Button button in _stackLayout.Children)
                    button.BackgroundColor = BackgroundColor;
                _stackLayout.OutlineColor = BackgroundColor;
            }
            else if (propertyName == TextColorProperty.PropertyName)
                foreach (Button button in _stackLayout.Children)
                    button.TextColor = TextColor;
            else if (propertyName == FontSizeProperty.PropertyName)
                foreach (Button button in _stackLayout.Children)
                    button.FontSize = FontSize;
            else if (propertyName == SeparatorColorProperty.PropertyName)
                _stackLayout.BackgroundColor = SeparatorColor;
            else if (propertyName == OutlineColorProperty.PropertyName)
                _stackLayout.OutlineColor = OutlineColor == Color.Default || OutlineColor == Color.Transparent ? BackgroundColor : OutlineColor;
            else if (propertyName == OutlineRadiusProperty.PropertyName)
                _stackLayout.OutlineRadius = OutlineRadius;
            else if (propertyName == SeparatorWidthProperty.PropertyName)
                _stackLayout.Spacing = SeparatorWidth;
            else if (propertyName == HapticEffectProperty.PropertyName)
                foreach (Button button in _stackLayout.Children)
                    button.HapticEffect = HapticEffect;
            else if (propertyName == HapticModeProperty.PropertyName)
                foreach (Button button in _stackLayout.Children)
                    button.HapticMode = HapticMode;
        }
        #endregion

        #region ButtonFactory
        Button CreateButton(Segment segment)
        {
            var button = new Button
            {
                Text = segment.Text,
                HtmlText = segment.HtmlText,
                IconImage = segment.IconImage,
                IconText = segment.IconText,
                Spacing = 4,
                TintIcon = true,
                HasTightSpacing = true,
                Padding = new Thickness(4, 0, 4, 4),
                TextColor = TextColor,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = BackgroundColor,
                Lines = 1,
                AutoFit = AutoFit.None,
            };
            button.SizeChanged += OnButtonSizeChanged;
            return button;
        }
        #endregion

        #region Collection Management
        void OnSegmentsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                        if (item is Segment segment)
                        {
                            var button = CreateButton(segment);
                            _stackLayout.Children.Add(button);
                            //_buttonSizes.Add(Size.Zero);
                        }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    if (e.OldItems.Count < e.NewItems.Count)
                        throw new System.Exception("e.OldItems.Count < e.NewItems.Count.  That's not expected.");
                    if (e.OldStartingIndex + e.OldItems.Count >= _stackLayout.Children.Count)
                        throw new System.Exception("(e.OldStartingIndex + e.OldItems.Count >= _buttons.Count.  That's not expected.");
                    
                    var tmpButtons = _stackLayout.Children.GetRange(e.OldStartingIndex, e.OldItems.Count);
                    _stackLayout.Children.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
                    _stackLayout.Children.InsertRange(e.NewStartingIndex, tmpButtons);
                    /*
                    var tmpSizes = _buttonSizes.GetRange(e.OldStartingIndex, e.OldItems.Count);
                    _buttonSizes.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
                    _buttonSizes.InsertRange(e.NewStartingIndex, tmpSizes);
                    */
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    tmpButtons = _stackLayout.Children.GetRange(e.OldStartingIndex, e.OldItems.Count);
                    foreach (var button in tmpButtons)
                        button.SizeChanged -= OnButtonSizeChanged;
                    _stackLayout.Children.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
                    //_buttonSizes.RemoveRange(e.OldStartingIndex, e.OldItems.Count);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    if (e.OldItems.Count < e.NewItems.Count)
                        throw new System.Exception("e.OldItems.Count < e.NewItems.Count.  That's not expected.");
                    if (e.OldStartingIndex + e.OldItems.Count >= _stackLayout.Children.Count)
                        throw new System.Exception("(e.OldStartingIndex + e.OldItems.Count >= _buttons.Count.  That's not expected.");
                    tmpButtons = _stackLayout.Children.GetRange(e.OldStartingIndex, e.OldItems.Count);
                    foreach (var button in tmpButtons)
                        button.SizeChanged -= OnButtonSizeChanged;
                    tmpButtons.Clear();
                    //tmpSizes = new List<Size>();
                    foreach (var item in e.NewItems)
                        if (item is Segment segment)
                        {
                            var button = CreateButton(segment);
                            tmpButtons.Add(button);
                            //tmpSizes.Add(Size.Zero);
                        };
                    if (tmpButtons != null && tmpButtons.Count > 0)
                    {
                        _stackLayout.Children.InsertRange(e.NewStartingIndex, tmpButtons);
                        //_buttonSizes.InsertRange(e.NewStartingIndex, tmpSizes);
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    _stackLayout.Children.Clear();
                    //_buttonSizes.Clear();
                    break;
            }
            UpdateButtonVisibilities();
        }
        #endregion

        #region Layout

        private void OnSizeChanged(object sender, System.EventArgs e)
        {
            //Cancel();
        }

        private void OnButtonSizeChanged(object sender, System.EventArgs e)
        {
            UpdateButtonVisibilities();
        }

        void UpdateButtonVisibilities()
        {
            if (Width < 0)
                return;
            // calculate pages
            bool multiPage=false;
            var width = 0.0;
            foreach (Button button in _stackLayout.Children)
            {
                width += button.UnexpandedTightSize.Width + SeparatorWidth;
                if (width >= Width)
                {
                    multiPage = true;
                    break;
                }
            }

            if (multiPage)
            {
                width = 0.0;
                foreach (Button button in _stackLayout.Children)
                //foreach (var button in _buttons)
                {
                    button.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    button.IsVisible = true;
                }
                var pages = 1;
                foreach (Button button in _stackLayout.Children)
                //foreach (var button in _buttons)
                {
                    width += button.Bounds.Width + SeparatorWidth;
                    if (width + _rightArrowButton.Width  >= Width)
                    {
                        pages++;
                        width = _leftArrowButton.Width;
                    }
                    button.IsVisible = _currentPage == pages - 1;
                }
            }
            else
            {
                foreach (Button button in _stackLayout.Children)
                    //foreach (var button in _buttons)
                    button.HorizontalOptions = LayoutOptions.Center;
            }
        }
        #endregion
    }
}