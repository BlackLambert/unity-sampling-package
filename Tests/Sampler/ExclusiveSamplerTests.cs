using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using UnityEngine;

namespace SBaier.Sampling.Tests.Sampler
{
    public class ExclusiveSamplerTests
    {
        private static List<object> _testDomains = new List<object>
        {
            new List<int>() { 1, 4, 3, 1, 3 },
            new List<string>() { "Dämon", "Kobold" },
            new List<Vector2>() { Vector2.zero, Vector2.down, Vector2.up, Vector2.up }
        };

        private static List<int> _samplesAmount = new List<int>()
        {
            5,
            0,
            2,
            4,
            1
        };

        [Test]
        public void ExclusiveSampler_UpdateDomain_UpdatesDomainOfBaseSampler<T>(
            [ValueSource(nameof(_testDomains))] List<T> domain)
        {
            Mock<Sampler<T>> baseSamplerMock = new Mock<Sampler<T>>();
            ExclusiveSampler<T> sampler = new ExclusiveSampler<T>(baseSamplerMock.Object);
            sampler.UpdateDomain(domain);
            baseSamplerMock.Verify(baseSampler => baseSampler.UpdateDomain(domain), Times.Once);
        }

        [Test]
        public void ExclusiveSampler_Sample_ThrowsExceptionIfDomainIsEmpty<T>(
            [ValueSource(nameof(_testDomains))] List<T> domain)
        {
            Mock<Sampler<T>> baseSamplerMock = new Mock<Sampler<T>>();
            ExclusiveSampler<T> sampler = new ExclusiveSampler<T>(baseSamplerMock.Object);
            baseSamplerMock.Setup(baseSampler => baseSampler.Sample()).Returns(domain.FirstOrDefault());
            sampler.UpdateDomain(new List<T>());
            Assert.Throws<ArgumentException>(() => sampler.Sample());
        }

        [Test]
        public void ExclusiveSampler_Sample_ReturnsValidSample<T>(
            [ValueSource(nameof(_testDomains))] List<T> domain)
        {
            Mock<Sampler<T>> baseSamplerMock = new Mock<Sampler<T>>();
            ExclusiveSampler<T> sampler = new ExclusiveSampler<T>(baseSamplerMock.Object);
            baseSamplerMock.Setup(baseSampler => baseSampler.Sample()).Returns(domain.FirstOrDefault());
            sampler.UpdateDomain(domain);
            T sample = sampler.Sample();
            Assert.IsTrue(domain.Contains(sample));
        }

        [Test]
        public void ExclusiveSampler_Sample_Multiple_ThrowsExceptionRequestedToManySamples<T>(
            [ValueSource(nameof(_testDomains))] List<T> domain)
        {
            Mock<Sampler<T>> baseSamplerMock = new Mock<Sampler<T>>();
            ExclusiveSampler<T> sampler = new ExclusiveSampler<T>(baseSamplerMock.Object);
            sampler.UpdateDomain(domain);
            Assert.Throws<ArgumentException>(() => sampler.Sample(domain.Count + 1));
        }

        [Test]
        public void ExclusiveSampler_Sample_Multiple_ReturnsRequestedSamples<T>(
            [ValueSource(nameof(_testDomains))] List<T> domain,
            [ValueSource(nameof(_samplesAmount))] int samplesAmount)
        {
            if (samplesAmount > domain.Count)
            {
                return;
            }

            Mock<Sampler<T>> baseSamplerMock = new Mock<Sampler<T>>();
            ExclusiveSampler<T> sampler = new ExclusiveSampler<T>(baseSamplerMock.Object);
            sampler.UpdateDomain(domain);
            baseSamplerMock.Setup(baseSampler => baseSampler.Sample(samplesAmount))
                .Returns(() => SelectSamples(domain, samplesAmount));
            List<T> samples = sampler.Sample(samplesAmount);
            Assert.AreEqual(samplesAmount, samples.Count);

            foreach (T sample in samples)
            {
                Assert.IsTrue(domain.Contains(sample));
            }
        }

        private List<T> SelectSamples<T>(List<T> domain, int samplesAmount)
        {
            List<T> result = new List<T>(samplesAmount);
            for (int i = 0; i < samplesAmount; i++)
            {
                result.Add(domain[i]);
            }
            return result;
        }
    }
}