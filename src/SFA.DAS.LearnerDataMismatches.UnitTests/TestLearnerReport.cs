﻿using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.LearnerDataMismatches.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.LearnerDataMismatches.UnitTests
{
    public class TestLearnerReport
    {
        private static IEnumerable<TestCaseData> IndividualPeriodValues()
        {
            static IEnumerable<(Func<ApprenticeshipBuilder> input, object expected)> cases()
            {
                yield return (
                    () => builder().WithProvider(ukprn: 12),
                    new { Ukprn = 12 });
            }

            static ApprenticeshipBuilder builder() => new ApprenticeshipBuilder();

            return cases().Select(x => new TestCaseData(x.input, x.expected));
        }

        [Test, TestCaseSource(nameof(IndividualPeriodValues))]
        public void B(Func<ApprenticeshipBuilder> build, object period)
        {
            var a = build();

            var sut = new LearnerReport(a.BuildApprentices(), a.BuildEarnings());

            sut.CollectionPeriods.Should().ContainEquivalentOf(
                new
                {
                    Apprenticeship = period,
                    Ilr = period,
                });
        }
    }
}