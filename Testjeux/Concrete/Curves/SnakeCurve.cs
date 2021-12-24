using Microsoft.Xna.Framework;

namespace GameNameSpace
{
	public sealed class SnakeCurve : ICurve, IStateContainer
	{
		private readonly float DefaultTtl;
		private readonly float Radius;
		private readonly float MaxWidth;
		private readonly float MiddleWidth;
		private readonly float MinWidth;
		private readonly float MaxHeight;
		private readonly float MiddleHeight;
		private readonly float MinHeight;

		// Points remarquables des trajectoires
		private readonly Vector2 pt0;
		private readonly Vector2 pt1;
		private readonly Vector2 pt2;
		private readonly Vector2 pt3;
		private readonly Vector2 pt4;
		private readonly Vector2 pt5;
		private readonly Vector2 pt6;
		private readonly Vector2 pt7;
		private readonly Vector2 pt8;

		// Trajectoires unitaires
		private readonly ICurve Curve0;
		private readonly ICurve Curve1;
		private readonly ICurve Curve2;
		private readonly ICurve Curve3;
		private readonly ICurve Curve4;
		private readonly ICurve Curve5;
		private readonly ICurve Curve6;
		private readonly ICurve Curve7;
		private readonly ICurve Curve8;
		private readonly ICurve Curve9;
		private readonly ICurve Curve10;
		private readonly ICurve Curve11;
		private readonly ICurve Curve12;
		private readonly ICurve Curve13;
		private readonly ICurve Curve14;
		private readonly ICurve Curve15;
		private readonly ICurve Curve16;

		public IStateItem CurrentState { get; set; }
		private readonly IStateItem InitialState;

		public SnakeCurve(float ttl, float textureRadius)
		{
			Point screen = Services.Instance.Get<IScreenService>().GetScreenSize();

			DefaultTtl = ttl;
			Radius = textureRadius;

			MaxWidth = screen.X - Radius * 1.5f;
			MiddleWidth = MaxWidth / 2.0f;
			MinWidth = Radius / 2.0f;

			MaxHeight = screen.Y - Radius;
			MiddleHeight = MaxHeight / 2.0f;
			MinHeight = Radius;

			// Calculs des points remarquables
			pt0 = new Vector2(MinWidth, MinHeight);
			pt1 = new Vector2(MinWidth, MiddleHeight);
			pt2 = new Vector2(MinWidth, MaxHeight);
			pt3 = new Vector2(MiddleWidth, MiddleHeight);
			pt4 = new Vector2(MiddleWidth, MinHeight);
			pt5 = new Vector2(MaxWidth, MaxHeight);
			pt6 = new Vector2(MaxWidth, MiddleHeight);
			pt7 = new Vector2(MaxWidth, MinHeight);
			pt8 = new Vector2(MaxWidth * 1.5f, MinHeight);

			// Calculs des trajectoires unitaires
			Curve0 = new BezierCurve(DefaultTtl, pt8, pt0, pt1);
			Curve1 = new BezierCurve(DefaultTtl, pt1, pt2, pt3);
			Curve2 = new BezierCurve(DefaultTtl, pt3, pt7, pt4);
			Curve3 = new BezierCurve(DefaultTtl, pt3, pt7, pt6);
			Curve4 = new BezierCurve(DefaultTtl, pt6, pt5, pt3);
			Curve5 = new BezierCurve(DefaultTtl, pt3, pt0, pt4);
			Curve6 = new BezierCurve(DefaultTtl, pt3, pt0, pt1);
			Curve7 = new BezierCurve(DefaultTtl, pt4, pt7, pt3);
			Curve8 = new BezierCurve(DefaultTtl, pt3, pt2, pt1);
			Curve9 = new BezierCurve(DefaultTtl, pt1, pt0, pt3);
			Curve10 = new BezierCurve(DefaultTtl, pt1, pt0, pt4);
			Curve11 = new BezierCurve(DefaultTtl, pt3, pt5, pt6);
			Curve12 = new BezierCurve(DefaultTtl, pt6, pt7, pt3);
			Curve13 = new BezierCurve(DefaultTtl, pt6, pt7, pt4);
			Curve14 = new BezierCurve(DefaultTtl, pt4, pt0, pt3);
			Curve15 = new BezierCurve(DefaultTtl, pt4, pt0, pt1);
			Curve16 = new BezierCurve(DefaultTtl, pt4, pt7, pt6);


			// Instanciation des différents états de la trajectoire globale
			IStateItem state0 = new SnakeCurveState(this, Curve0, Curve1);
			IStateItem state1 = new SnakeCurveState(this, Curve2);
			IStateItem state2 = new SnakeCurveState(this, Curve3, Curve4);
			IStateItem state3 = new SnakeCurveState(this, Curve5);
			IStateItem state4 = new SnakeCurveState(this, Curve6, Curve1);
			IStateItem state5 = new SnakeCurveState(this, Curve14, Curve11);
			IStateItem state6 = new SnakeCurveState(this, Curve15, Curve1);
			IStateItem state7 = new SnakeCurveState(this, Curve12, Curve8);
			IStateItem state8 = new SnakeCurveState(this, Curve13);
			IStateItem state9 = new SnakeCurveState(this, Curve10);
			IStateItem state10 = new SnakeCurveState(this, Curve9, Curve11);
			IStateItem state11 = new SnakeCurveState(this, Curve7, Curve8);
			IStateItem state12 = new SnakeCurveState(this, Curve16, Curve4);

			// Transitions entre les états
			state0.Transitions.Add(state1);
			state0.Transitions.Add(state2);
			state1.Transitions.Add(state5);
			state1.Transitions.Add(state6);
			state2.Transitions.Add(state3);
			state2.Transitions.Add(state4);
			state3.Transitions.Add(state11);
			state3.Transitions.Add(state12);
			state4.Transitions.Add(state1);
			state4.Transitions.Add(state2);
			state5.Transitions.Add(state7);
			state5.Transitions.Add(state8);
			state6.Transitions.Add(state1);
			state6.Transitions.Add(state2);
			state7.Transitions.Add(state9);
			state7.Transitions.Add(state10);
			state8.Transitions.Add(state5);
			state8.Transitions.Add(state6);
			state9.Transitions.Add(state11);
			state9.Transitions.Add(state12);
			state10.Transitions.Add(state7);
			state10.Transitions.Add(state8);
			state11.Transitions.Add(state9);
			state11.Transitions.Add(state10);
			state12.Transitions.Add(state3);
			state12.Transitions.Add(state4);

			// Etat initial
			CurrentState = InitialState = state0;
		}

		public float Ttl => 0.0f;

		public bool Ended => false;

		public Vector2 Position => (CurrentState as ICurve).Position;

		public void Reset()
		{
			CurrentState = InitialState;
		}

		public void Update(GameTime gameTime)
		{
			(CurrentState as ICurve).Update(gameTime);
		}
	}
}