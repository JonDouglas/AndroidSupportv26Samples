using Android.App;
using Android.Graphics.Drawables;
using Android.Widget;
using Android.OS;

namespace AndroidToAppleVectorLogo
{
    [Activity(Label = "AndroidToAppleVectorLogo", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private AnimatedVectorDrawable _mightyMorphinAnimatedVectorDrawable;
        private AnimatedVectorDrawable _mightyMorphinAnimatedVectorDrawableReversed;
        private ImageView _animatorImageView;
        private bool _isShowingAndroid = true;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _animatorImageView = FindViewById<ImageView>(Resource.Id.path_morph_animated_vector);
            _mightyMorphinAnimatedVectorDrawable =
                (AnimatedVectorDrawable) GetDrawable(Resource.Drawable.consolidated_animated_vector);
            _mightyMorphinAnimatedVectorDrawableReversed =
                (AnimatedVectorDrawable) GetDrawable(Resource.Drawable.consolidated_animated_vector_reverse);
            _animatorImageView.Click += (sender, args) =>
            {
                AnimatedVectorDrawable prevDrawable = _isShowingAndroid
                    ? _mightyMorphinAnimatedVectorDrawableReversed
                    : _mightyMorphinAnimatedVectorDrawable;

                if (prevDrawable.IsRunning)
                {
                    prevDrawable.Stop();
                }

                AnimatedVectorDrawable currentDrawable = _isShowingAndroid
                    ? _mightyMorphinAnimatedVectorDrawable
                    : _mightyMorphinAnimatedVectorDrawableReversed;
                _animatorImageView.SetImageDrawable(currentDrawable);
                currentDrawable.Start();
                _isShowingAndroid = !_isShowingAndroid;
            };
        }
    }
}

