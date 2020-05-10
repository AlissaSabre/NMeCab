using NMeCab;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LibNMeCabTest
{
    public class IpaDicTaggerTest : IDisposable
    {
        private const string dicDir = "../../../../../dic/ipadic";

        private MeCabIpaDicTagger tagger;

        public IpaDicTaggerTest()
        {
            this.tagger = MeCabIpaDicTagger.Create(dicDir);
        }

        [Fact]
        public void NodeProperty()
        {
            var node = new MeCabIpaDicNode()
            {
                Feature = "1,2,3,4,5,6,7,8,9"
            };

            Assert.Equal("1", node.PartsOfSpeech);
            Assert.Equal("2", node.PartsOfSpeechSection1);
            Assert.Equal("3", node.PartsOfSpeechSection2);
            Assert.Equal("4", node.PartsOfSpeechSection3);
            Assert.Equal("5", node.ConjugatedForm);
            Assert.Equal("6", node.Inflection);
            Assert.Equal("7", node.OriginalForm);
            Assert.Equal("8", node.Reading);
            Assert.Equal("9", node.Pronounciation);
        }

        [Fact]
        public void ParseTest()
        {
            var node = this.tagger.Parse("すもも")[0];

            Assert.Equal("名詞", node.PartsOfSpeech);
            Assert.Equal("一般", node.PartsOfSpeechSection1);
            Assert.Equal("*", node.PartsOfSpeechSection2);
            Assert.Equal("*", node.PartsOfSpeechSection3);
            Assert.Equal("*", node.ConjugatedForm);
            Assert.Equal("*", node.Inflection);
            Assert.Equal("すもも", node.OriginalForm);
            Assert.Equal("スモモ", node.Reading);
            Assert.Equal("スモモ", node.Pronounciation);
        }

        public void Dispose()
        {
            this.tagger?.Dispose();
        }
    }
}
