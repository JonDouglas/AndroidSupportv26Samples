using Android.App;
using Android.Graphics.Drawables;
using Android.Widget;
using Android.OS;
using Android.Support.Graphics.Drawable;
using Android.Support.V7.App;

namespace EndlessPinJump
{
    [Activity(Label = "EndlessPinJump", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var avd = AnimatedVectorDrawableCompat.Create(this, Resource.Drawable.avd_endless_pin_jump);
            var iv = FindViewById<ImageView>(Resource.Id.pin);
            iv.SetImageDrawable(avd);
            avd.RegisterAnimationCallback(new AnimatedCallback(iv, avd));
            avd.Start();
        }
    }

    public class AnimatedCallback : Animatable2CompatAnimationCallback
    {
        private ImageView imageView;
        private AnimatedVectorDrawableCompat vectorDrawable;
        public AnimatedCallback(ImageView iv, AnimatedVectorDrawableCompat avd)
        {
            imageView = iv;
            vectorDrawable = avd;
        }
        public override void OnAnimationEnd(Drawable drawable)
        {
            imageView.Post(vectorDrawable.Start);
        }
    }
}

