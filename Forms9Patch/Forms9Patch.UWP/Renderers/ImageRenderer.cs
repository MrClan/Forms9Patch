﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Forms9Patch.Image), typeof(Forms9Patch.UWP.ImageRenderer))]
namespace Forms9Patch.UWP
{
    public class ImageRenderer : ViewRenderer<Image, ImageView>
    {
        bool _measured;
        bool _disposed;

        bool _debugMessages=false;

        static int _instances;
        int _instance;

        public ImageRenderer()
        {
            _instance = _instances++;
        }

        /*
        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer.GetDesiredSize(" + widthConstraint + ", "+heightConstraint + ") ENTER");

            if (Control?.Source is WriteableBitmap bitmap)
            {
                if (bitmap.PixelWidth < 1 || bitmap.PixelHeight < 1)
                {
                    if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer.GetDesiredSize(" + widthConstraint + ", " + heightConstraint + ") RETURN: "+new SizeRequest());
                    return new SizeRequest();
                }
                _measured = true;
                Xamarin.Forms.Size result = new Xamarin.Forms.Size { Width = bitmap.PixelWidth, Height = bitmap.PixelHeight };
                if (Element.Fill != Fill.None)
                {
                    if (!Double.IsInfinity(widthConstraint))
                        result.Width = Math.Max(bitmap.PixelWidth, widthConstraint);
                    if (!Double.IsInfinity(heightConstraint))
                        result.Height = Math.Max(bitmap.PixelHeight, heightConstraint);
                }
                {
                    if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer.GetDesiredSize(" + widthConstraint + ", " + heightConstraint + ") RETURN: " + new SizeRequest(result));
                    return new SizeRequest(result);
                }
            }
            else
            {
                if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer.GetDesiredSize(" + widthConstraint + ", " + heightConstraint + ") RETURN: " + new SizeRequest());
                return new SizeRequest();
            }
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                Control?.Dispose();
                SetNativeControl(null);
                _disposed = true;
            }

            base.Dispose(disposing);
        }

        protected override async void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer["+_instance+"].OnElementChanged(["+e.OldElement+", "+e.NewElement+"]");
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                    SetNativeControl(new ImageView(_instance));

                await TryUpdateSource();
                UpdateAspect();
                UpdateCapInsets();
            }
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].OnElementChanged([" + e.OldElement + ", " + e.NewElement + "] RETURN");
        }


        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].OnElementPropertyChanged([" + e.PropertyName+"]");
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Image.SourceProperty.PropertyName)
                await TryUpdateSource();
            /*
            else if (e.PropertyName == Image.AspectProperty.PropertyName)
                UpdateAspect();
                */
            else if (e.PropertyName == Image.FillProperty.PropertyName)
                UpdateAspect();
            else if (e.PropertyName == Image.CapInsetsProperty.PropertyName)
                UpdateCapInsets();

            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].OnElementPropertyChanged([" + e.PropertyName + "] RETURN");
        }

        /*
        static Stretch GetStretch(Aspect aspect)
        {
            switch (aspect)
            {
                case Aspect.Fill:
                    return Stretch.Fill;
                case Aspect.AspectFill:
                    return Stretch.UniformToFill;
                default:
                case Aspect.AspectFit:
                    return Stretch.Uniform;
            }
        }
        */

            /*
        void OnImageOpened(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer.OnImageOpened("+routedEventArgs+")");
            if (_measured)
                RefreshImage();
            ((IElementController)Element).SetValueFromRenderer(Xamarin.Forms.Image.IsLoadingProperty, false);
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer.OnImageOpened(" + routedEventArgs + ") RETURN");
        }

        protected virtual void OnImageFailed(object sender, ExceptionRoutedEventArgs exceptionRoutedEventArgs)
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer.OnImageFaied("+exceptionRoutedEventArgs+")");
            System.Diagnostics.Debug.WriteLine ("Image Loading", $"Image failed to load: {exceptionRoutedEventArgs.ErrorMessage}");
            ((IElementController)Element).SetValueFromRenderer(Xamarin.Forms.Image.IsLoadingProperty, false);
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer.OnImageFaied(" + exceptionRoutedEventArgs + ") RETURN");
        }
        */

        void RefreshImage()
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].RefreshImage()");
            ((IVisualElementController)Element)?.InvalidateMeasure(InvalidationTrigger.RendererReady);
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].RefreshImage() RETURN ");
        }

        private void UpdateCapInsets()
        {
            if (Control != null)
                Control.CapInsets = Element.CapInsets;
        }


        void UpdateAspect()
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].UpdateAspect()");
            /*

if (_disposed || Element == null || Control == null)
{
    return;
}

if (Element.Fill == Fill.Tile || Element.Fill == Fill.None)
{
    Control.HorizontalAlignment = HorizontalAlignment.Left;
    Control.VerticalAlignment = VerticalAlignment.Top;
}
else
{
    Control.HorizontalAlignment = HorizontalAlignment.Center;
    Control.VerticalAlignment = VerticalAlignment.Center;
}

if (Control.NineGrid.IsANineGrid())
    Control.Stretch = Stretch.Fill;
else
    Control.Stretch = Element.Fill.ToStretch();
    */

            if (Control!=null)
                Control.Fill = Element.Fill;

            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].UpdateAspect() RETURN");
        }

        protected virtual async Task TryUpdateSource()
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].TryUpdateSource()");
            // By default we'll just catch and log any exceptions thrown by UpdateSource so we don't bring down
            // the application; a custom renderer can override this method and handle exceptions from
            // UpdateSource differently if it wants to

            try
            {
                await UpdateSource().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(nameof(ImageRenderer), "ImageRenderer[" + _instance + "].TryUpdateSource() Error loading image: {0}", ex);
            }
            finally
            {
                ((IImageController)Element)?.SetIsLoading(false);
            }
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].TryUpdateSource() RETURN");
        }

        protected async Task UpdateSource()
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].UpdateSource()");
            if (_disposed || Element == null || Control == null)
            {
                System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].UpdateSource() RETURN");
                return;
            }
           
            Stopwatch stopwatch = Stopwatch.StartNew();

            //Element.SetIsLoading(true);
            ((IElementController)Element).SetValueFromRenderer(Xamarin.Forms.Image.IsLoadingProperty, true);

            Xamarin.Forms.ImageSource source = Element.Source;

            await Control.SetSourceAsync(source);
            
            RefreshImage();



            //Element.SetIsLoading(false);
            ((IElementController)Element).SetValueFromRenderer(Xamarin.Forms.Image.IsLoadingProperty, false);

            stopwatch.Stop();
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].UpdateSource() RETURN elapsedTime=["+stopwatch.ElapsedMilliseconds+"]");

        }

        Windows.Foundation.Size _lastFinalSize = Windows.Foundation.Size.Empty;
        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].ArrangeOverride(" + finalSize+") ENTER/RETURN");

            if (_lastFinalSize!=finalSize && Element.Fill == Fill.Tile && finalSize.Width > 0 && finalSize.Height > 0 && !Double.IsInfinity(finalSize.Width) && !Double.IsInfinity(finalSize.Height) && !Double.IsNaN(finalSize.Width) && !Double.IsNaN(finalSize.Height))
                Control.GenerateLayout(finalSize);
            _lastFinalSize = finalSize;
            return base.ArrangeOverride(finalSize);
        }

        protected override Windows.Foundation.Size MeasureOverride(Windows.Foundation.Size availableSize)
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].MeasureOverride(" + availableSize + ") ENTER/RETURN");
            return base.MeasureOverride(availableSize);
        }

        protected override void UpdateBackgroundColor()
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].UpdateBackgroundColor() ENTER/RETURN");
            base.UpdateBackgroundColor();
        }

        protected override void UpdateNativeControl()
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].UpdateNativeControl() ENTER/RETURN");
            base.UpdateNativeControl();
        }

        protected override void OnApplyTemplate()
        {
            if (_debugMessages) System.Diagnostics.Debug.WriteLine("ImageRenderer[" + _instance + "].OnApplyTemplate() ENTER/RETURN");
            base.OnApplyTemplate();
        }
    }
}