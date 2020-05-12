// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Game.Audio;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Tau.Objects
{
    public class Slider : TauHitObject, IHasCurve
    {
        public double EndTime
        {
            get => StartTime + this.SpanCount() * Path.Distance;
            set => throw new System.NotSupportedException($"Adjust via {nameof(RepeatCount)} instead"); // can be implemented if/when needed.
        }

        public double Duration => EndTime - StartTime;

        public double Distance => Path.Distance;

        public int RepeatCount { get; set; }

        public List<IList<HitSampleInfo>> NodeSamples { get; set; } = new List<IList<HitSampleInfo>>();

        private readonly SliderPath path = new SliderPath();

        public SliderPath Path
        {
            get => path;
            set
            {
                path.ControlPoints.Clear();
                path.ExpectedDistance.Value = null;

                if (value != null)
                {
                    path.ControlPoints.AddRange(value.ControlPoints.Select(c => new PathControlPoint(c.Position.Value, c.Type.Value)));
                    path.ExpectedDistance.Value = value.ExpectedDistance.Value;
                }
            }
        }

        public Slider()
        {
            SamplesBindable.CollectionChanged += (_, __) => updateNestedSamples();
        }

        private void updateNestedSamples()
        {
            foreach (var repeat in NestedHitObjects.OfType<SliderRepeat>())
                repeat.Samples = getNodeSamples(repeat.RepeatIndex + 1);
        }

        private IList<HitSampleInfo> getNodeSamples(int nodeIndex) =>
            nodeIndex < NodeSamples.Count ? NodeSamples[nodeIndex] : Samples;

        protected override HitWindows CreateHitWindows() => HitWindows.Empty;
    }
}
