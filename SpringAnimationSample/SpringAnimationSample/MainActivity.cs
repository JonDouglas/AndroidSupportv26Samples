using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Animation;
using Android.Views;
using Java.Lang;

namespace SpringAnimationSample
{
    [Activity(Label = "SpringAnimationSample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private float downX, downY;
        private SeekBar damping, stiffness;
        private VelocityTracker velocityTracker;

        private float Stiffness => Math.Max(stiffness.Progress, 1f);
        private float Damping => damping.Progress / 100f;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            FindViewById(Android.Resource.Id.Content).SystemUiVisibility =
                (StatusBarVisibility)(SystemUiFlags.LayoutStable | SystemUiFlags.LayoutFullscreen | SystemUiFlags.HideNavigation);
            stiffness = FindViewById<SeekBar>(Resource.Id.stiffness);
            damping = FindViewById<SeekBar>(Resource.Id.damping);
            velocityTracker = VelocityTracker.Obtain();
            View box = FindViewById(Resource.Id.box);
            box.Touch += (sender, args) =>
            {
                switch (args.Event.Action)
                {
                    case MotionEventActions.Down:
                        downX = args.Event.GetX();
                        downY = args.Event.GetY();
                        velocityTracker.AddMovement(args.Event);
                        break;
                    case MotionEventActions.Move:
                        box.TranslationX = args.Event.GetX() - downX;
                        box.TranslationY = args.Event.GetY() - downY;
                        velocityTracker.AddMovement(args.Event);
                        break;
                    case MotionEventActions.Up:
                    case MotionEventActions.Cancel:
                        velocityTracker.ComputeCurrentVelocity(1000);
                        if (box.TranslationX != 0)
                        {
                            SpringAnimation animX = new SpringAnimation(box, DynamicAnimation.TranslationX, 0);
                            animX.Spring.SetStiffness(Stiffness);
                            animX.Spring.SetDampingRatio(Damping);
                            animX.SetStartVelocity(velocityTracker.XVelocity);
                            animX.Start();
                        }
                        if (box.TranslationY != 0)
                        {
                            SpringAnimation animY = new SpringAnimation(box, DynamicAnimation.TranslationY, 0);
                            animY.Spring.SetStiffness(Stiffness);
                            animY.Spring.SetDampingRatio(Damping);
                            animY.SetStartVelocity(velocityTracker.YVelocity);
                            animY.Start();
                        }
                        velocityTracker.Clear();
                        break;

                }
            };
        }
    }
}

