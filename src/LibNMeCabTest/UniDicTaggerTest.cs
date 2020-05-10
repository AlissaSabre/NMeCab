using NMeCab;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LibNMeCabTest
{
    public class UniDicTaggerTest : IDisposable
    {
        private const string dicDir = "../../../../../dic/unidic-2.1.2";

        private MeCabUniDicTagger tagger;

        public UniDicTaggerTest()
        {
            this.tagger = MeCabUniDicTagger.Create(dicDir);
        }

        [Fact]
        public void NodeProperty()
        {
            var node = new MeCabUniDicNode()
            {
                Feature = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26"
            };

            Assert.Equal("1", node.Pos1);
            Assert.Equal("2", node.Pos2);
            Assert.Equal("3", node.Pos3);
            Assert.Equal("4", node.Pos4);
            Assert.Equal("5", node.CType);
            Assert.Equal("6", node.CForm);
            Assert.Equal("7", node.LForm);
            Assert.Equal("8", node.Lemma);
            Assert.Equal("9", node.Orth);
            Assert.Equal("10", node.Pron);
            Assert.Equal("11", node.OrthBase);
            Assert.Equal("12", node.PronBase);
            Assert.Equal("13", node.Goshu);
            Assert.Equal("14", node.IType);
            Assert.Equal("15", node.IForm);
            Assert.Equal("16", node.FType);
            Assert.Equal("17", node.FForm);
            Assert.Equal("18", node.Kana);
            Assert.Equal("19", node.KanaBase);
            Assert.Equal("20", node.Form);
            Assert.Equal("21", node.FormBase);
            Assert.Equal("22", node.IConType);
            Assert.Equal("23", node.FConType);
            Assert.Equal("24", node.AType);
            Assert.Equal("25", node.AConType);
            Assert.Equal("26", node.AModType);
        }

        [Fact]
        public void ParseTest()
        {
            var node = this.tagger.Parse("走る")[0];

            Assert.Equal("動詞", node.Pos1);
            Assert.Equal("一般", node.Pos2);
            Assert.Equal("*", node.Pos3);
            Assert.Equal("*", node.Pos4);
            Assert.Equal("五段-ラ行", node.CType);
            Assert.Equal("終止形-一般", node.CForm);
            Assert.Equal("ハシル", node.LForm);
            Assert.Equal("走る", node.Lemma);
            Assert.Equal("走る", node.Orth);
            Assert.Equal("ハシル", node.Pron);
            Assert.Equal("走る", node.OrthBase);
            Assert.Equal("ハシル", node.PronBase);
            Assert.Equal("和", node.Goshu);
            Assert.Equal("ハ濁", node.IType);
            Assert.Equal("基本形", node.IForm);
            Assert.Equal("*", node.FType);
            Assert.Equal("*", node.FForm);
            Assert.Equal("ハシル", node.Kana);
            Assert.Equal("ハシル", node.KanaBase);
            Assert.Equal("ハシル", node.Form);
            Assert.Equal("ハシル", node.FormBase);
            Assert.Equal("*", node.IConType);
            Assert.Equal("*", node.FConType);
            Assert.Equal("2", node.AType);
            Assert.Equal("C1", node.AConType);
            Assert.Equal("*", node.AModType);
        }

        public void Dispose()
        {
            this.tagger?.Dispose();
        }
    }
}
