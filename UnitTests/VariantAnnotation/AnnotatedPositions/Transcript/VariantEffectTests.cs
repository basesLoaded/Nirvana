﻿using Moq;
using VariantAnnotation.AnnotatedPositions.Transcript;
using VariantAnnotation.Interface.AnnotatedPositions;
using VariantAnnotation.Interface.Positions;
using Xunit;

namespace UnitTests.VariantAnnotation.AnnotatedPositions.Transcript
{
    public class VariantEffectTests
    {
        [Theory]
        [InlineData(false,false,false,false)]
        [InlineData(false,false,true,true)]
        [InlineData(true, false, true, false)]
        [InlineData(true, true, true, true)]
        public void IsSpliceAcceptorVariant(bool onReverseStrand,bool isStartSpliceSite, bool isEndSpliceSite,bool expectedResult)
        {
            var positionalEffect = new TranscriptPositionalEffect
            {
                IsStartSpliceSite = isStartSpliceSite,
                IsEndSpliceSite = isEndSpliceSite
            };

            var variant = new Mock<ISimpleVariant>();
            var transcript = new Mock<ITranscript>();

            variant.SetupGet(x => x.AltAllele).Returns("G");
            variant.SetupGet(x => x.RefAllele).Returns("C");

            var variantEffect = new VariantEffect(positionalEffect, variant.Object, transcript.Object, "", "", "", "",
                null);

            var gene = new Mock<IGene>();
            transcript.SetupGet(x => x.Gene).Returns(gene.Object);
            gene.SetupGet(x => x.OnReverseStrand).Returns(onReverseStrand);

            Assert.Equal(expectedResult, variantEffect.IsSpliceAcceptorVariant());
        }

        [Theory]
        [InlineData(false, false, false, false)]
        [InlineData(false, true, true, true)]
        [InlineData(true, false, false, false)]
        [InlineData(true, true, true, true)]
        public void IsSpliceDonorVariant(bool onReverseStrand, bool isStartSpliceSite, bool isEndSpliceSite, bool expectedResult)
        {
            var positionalEffect = new TranscriptPositionalEffect
            {
                IsStartSpliceSite = isStartSpliceSite,
                IsEndSpliceSite = isEndSpliceSite
            };

            var variant = new Mock<ISimpleVariant>();
            var transcript = new Mock<ITranscript>();

            variant.SetupGet(x => x.AltAllele).Returns("G");
            variant.SetupGet(x => x.RefAllele).Returns("C");

            var variantEffect = new VariantEffect(positionalEffect, variant.Object, transcript.Object, "", "", "", "",
                null);

            var gene = new Mock<IGene>();
            transcript.SetupGet(x => x.Gene).Returns(gene.Object);
            gene.SetupGet(x => x.OnReverseStrand).Returns(onReverseStrand);

            Assert.Equal(expectedResult, variantEffect.IsSpliceDonorVariant());

        }


        [Theory]
        [InlineData(false, true, false, false, false)]
        [InlineData(false, true, true, true, true)]
        [InlineData(false, false, true, true, false)]
        [InlineData(true, true, false, false, false)]
        [InlineData(true, true, true, true, true)]
        [InlineData(true, false, true, true, false)]
        public void IsFivePrimeUtrVariant(bool onReverseStrand, bool withinCdna, bool beforeCoding, bool afterCoding,
            bool expectedResult)
        {
            var positionalEffect = new TranscriptPositionalEffect
            {
                BeforeCoding = beforeCoding,
                AfterCoding = afterCoding,
                WithinCdna = withinCdna
            };

            var variant = new Mock<ISimpleVariant>();
            var transcript = new Mock<ITranscript>();

            variant.SetupGet(x => x.AltAllele).Returns("G");
            variant.SetupGet(x => x.RefAllele).Returns("C");

            var variantEffect = new VariantEffect(positionalEffect, variant.Object, transcript.Object, "", "", "", "",
                null);

            var gene = new Mock<IGene>();
            transcript.SetupGet(x => x.Gene).Returns(gene.Object);
            gene.SetupGet(x => x.OnReverseStrand).Returns(onReverseStrand);

            var translation = new Mock<ITranslation>();
            transcript.SetupGet(x => x.Translation).Returns(translation.Object);

            Assert.Equal(expectedResult, variantEffect.IsFivePrimeUtrVariant());
        }


        //[Theory]
        //public void IsFrameShiftVariant(bool isCoding,bool isIncompleteTerminalCodonVariant,bool hasFrameShift,bool isStopRetained,bool isTrucatedByStop,bool expected)
        //{
        //    var positionalEffect = new TranscriptPositionalEffect
        //    {
        //        IsCoding = isCoding,
        //        HasFrameShift = hasFrameShift

        //    };

        //    var cache = new VariantEffectCache();
        //    cache.Add(ConsequenceTag.incomplete_terminal_codon_variant,isIncompleteTerminalCodonVariant);
        //    cache.Add(ConsequenceTag.stop_retained_variant, isStopRetained);
        //    //cache.Add();



        //}


    }
}