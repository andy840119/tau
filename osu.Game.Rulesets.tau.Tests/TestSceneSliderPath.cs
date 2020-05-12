// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Lines;
using osu.Framework.Testing;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osuTK;

namespace osu.Game.Rulesets.Tau.Tests
{
    public class TestSceneSliderPath : TestScene
    {
        private readonly List<Vector2> pathList = new List<Vector2>(new[]
        {
            Vector2.Zero,
            new Vector2(0, 50),
            new Vector2(50, 50)
        });

        public TestSceneSliderPath()
        {
            SmoothPath slider;

            var path = new SliderPath(PathType.Bezier, pathList.ToArray());
            path.GetPathToProgress(pathList, 0, 1);

            Add(slider = new SmoothPath
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.TopLeft,
                PathRadius = 2.5f,
                Vertices = pathList
            });

            AddSliderStep("Progress", 0.0f, 1.0f, 0.0f, p =>
            {
                path.GetPathToProgress(pathList, p, 1);
                slider.Vertices = pathList;
            });
        }
    }
}
