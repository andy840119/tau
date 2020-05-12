// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Lines;
using osuTK;

namespace osu.Game.Rulesets.Tau.Objects.Drawables
{
    public class DrawableSlider : DrawabletauHitObject
    {
        private Slider slider;

        public SmoothPath DrawablePath;
        public readonly List<Vector2> CurrentCurve = new List<Vector2>();

        public DrawableSlider(Slider hitObject)
            : base(hitObject)
        {
            slider = hitObject;

            slider.Path.GetPathToProgress(CurrentCurve, 0, 1);

            AddInternal(DrawablePath = new SmoothPath
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.TopLeft,
                PathRadius = 2.5f,
                Vertices = CurrentCurve
            });
        }

        protected override void Update()
        {
            base.Update();

            double completionProgress = Math.Clamp((Time.Current - slider.StartTime) / slider.Duration, 0, 1);
            slider.Path.GetPathToProgress(CurrentCurve, completionProgress, 1);
            DrawablePath.Vertices = CurrentCurve;
        }
    }
}
