// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics.Sprites;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Tau.Objects;

namespace osu.Game.Rulesets.Tau.Beatmaps
{
    public class TauBeatmap : Beatmap<TauHitObject>
    {
        public override IEnumerable<BeatmapStatistic> GetStatistics()
        {
            int beats = HitObjects.Count(b => b != null);
            int sliders = HitObjects.Count(b => b is Slider);

            return new[]
            {
                new BeatmapStatistic
                {
                    Name = "Total Beats count",
                    Content = beats.ToString(),
                    Icon = FontAwesome.Solid.Square
                },
                new BeatmapStatistic
                {
                    Name = "Slider count",
                    Content = sliders.ToString(),
                    Icon = FontAwesome.Regular.Circle
                }
            };
        }
    }
}
