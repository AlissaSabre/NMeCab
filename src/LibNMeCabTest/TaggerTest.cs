using NMeCab;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace LibNMeCabTest
{
    public class TaggerTest : IDisposable
    {
        private const string dicDir = "../../../../../dic/ipadic";

        private MeCabTagger tagger;

        public TaggerTest()
        {
            this.tagger = MeCabTagger.Create(dicDir);
        }

        [Fact]
        public void OneBest()
        {
            var nodes = this.tagger.Parse("すもももももももものうち");

            Assert.Equal(7, nodes.Length);
            Assert.Equal("すもも", nodes[0].Surface);
            Assert.Equal("名詞,一般,*,*,*,*,すもも,スモモ,スモモ", nodes[0].Feature);
            Assert.Equal("も", nodes[1].Surface);
            Assert.Equal("助詞,係助詞,*,*,*,*,も,モ,モ", nodes[1].Feature);
            Assert.Equal("もも", nodes[2].Surface);
            Assert.Equal("名詞,一般,*,*,*,*,もも,モモ,モモ", nodes[2].Feature);
            Assert.Equal("も", nodes[3].Surface);
            Assert.Equal("助詞,係助詞,*,*,*,*,も,モ,モ", nodes[3].Feature);
            Assert.Equal("もも", nodes[4].Surface);
            Assert.Equal("名詞,一般,*,*,*,*,もも,モモ,モモ", nodes[4].Feature);
            Assert.Equal("の", nodes[5].Surface);
            Assert.Equal("助詞,連体化,*,*,*,*,の,ノ,ノ", nodes[5].Feature);
            Assert.Equal("うち", nodes[6].Surface);
            Assert.Equal("名詞,非自立,副詞可能,*,*,*,うち,ウチ,ウチ", nodes[6].Feature);

            Assert.True(nodes[0].Prev.IsBest);
            Assert.True(nodes[1].IsBest);
            Assert.True(nodes[2].IsBest);
            Assert.True(nodes[3].IsBest);
            Assert.True(nodes[4].IsBest);
            Assert.True(nodes[5].IsBest);
            Assert.True(nodes[6].IsBest);
            Assert.True(nodes[6].Next.IsBest);

            Assert.Equal(MeCabNodeStat.Bos, nodes[0].Prev.Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes[0].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes[1].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes[2].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes[3].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes[4].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes[5].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes[6].Stat);
            Assert.Equal(MeCabNodeStat.Eos, nodes[6].Next.Stat);

            var tmp = nodes[0].Next;
            Assert.Same(nodes[1], tmp);
            tmp = tmp.Next;
            Assert.Same(nodes[2], tmp);
            tmp = tmp.Next;
            Assert.Same(nodes[3], tmp);
            tmp = tmp.Next;
            Assert.Same(nodes[4], tmp);
            tmp = tmp.Next;
            Assert.Same(nodes[5], tmp);
            tmp = tmp.Next;
            Assert.Same(nodes[6], tmp);
        }

        [Fact]
        public void UnknownSpeech()
        {
            var nodes = this.tagger.Parse("今日はエヌメカブを使った");

            Assert.Equal(6, nodes.Length);
            Assert.Equal("今日", nodes[0].Surface);
            Assert.Equal(MeCabNodeStat.Nor, nodes[0].Stat);
            Assert.Equal("は", nodes[1].Surface);
            Assert.Equal(MeCabNodeStat.Nor, nodes[1].Stat);
            Assert.Equal("エヌメカブ", nodes[2].Surface);
            Assert.NotNull(nodes[2].Feature);
            Assert.Equal(MeCabNodeStat.Unk, nodes[2].Stat);
            Assert.Equal("を", nodes[3].Surface);
            Assert.Equal(MeCabNodeStat.Nor, nodes[3].Stat);
            Assert.Equal("使っ", nodes[4].Surface);
            Assert.Equal(MeCabNodeStat.Nor, nodes[4].Stat);
            Assert.Equal("た", nodes[5].Surface);
            Assert.Equal(MeCabNodeStat.Nor, nodes[5].Stat);
        }

        [Fact]
        public void NBest()
        {
            var enumerator = this.tagger.ParseNBest("すもももももももものうち").GetEnumerator();

            Assert.True(enumerator.MoveNext());
            var nodes1 = enumerator.Current;

            Assert.Equal(7, nodes1.Length);
            Assert.Equal("すもも", nodes1[0].Surface);
            Assert.Equal("名詞,一般,*,*,*,*,すもも,スモモ,スモモ", nodes1[0].Feature);
            Assert.Equal("も", nodes1[1].Surface);
            Assert.Equal("助詞,係助詞,*,*,*,*,も,モ,モ", nodes1[1].Feature);
            Assert.Equal("もも", nodes1[2].Surface);
            Assert.Equal("名詞,一般,*,*,*,*,もも,モモ,モモ", nodes1[2].Feature);
            Assert.Equal("も", nodes1[3].Surface);
            Assert.Equal("助詞,係助詞,*,*,*,*,も,モ,モ", nodes1[3].Feature);
            Assert.Equal("もも", nodes1[4].Surface);
            Assert.Equal("名詞,一般,*,*,*,*,もも,モモ,モモ", nodes1[4].Feature);
            Assert.Equal("の", nodes1[5].Surface);
            Assert.Equal("助詞,連体化,*,*,*,*,の,ノ,ノ", nodes1[5].Feature);
            Assert.Equal("うち", nodes1[6].Surface);
            Assert.Equal("名詞,非自立,副詞可能,*,*,*,うち,ウチ,ウチ", nodes1[6].Feature);

            Assert.True(nodes1[0].Prev.IsBest);
            Assert.True(nodes1[1].IsBest);
            Assert.True(nodes1[2].IsBest);
            Assert.True(nodes1[3].IsBest);
            Assert.True(nodes1[4].IsBest);
            Assert.True(nodes1[5].IsBest);
            Assert.True(nodes1[6].IsBest);
            Assert.True(nodes1[6].Next.IsBest);

            Assert.Equal(MeCabNodeStat.Bos, nodes1[0].Prev.Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes1[0].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes1[1].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes1[2].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes1[3].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes1[4].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes1[5].Stat);
            Assert.Equal(MeCabNodeStat.Nor, nodes1[6].Stat);
            Assert.Equal(MeCabNodeStat.Eos, nodes1[6].Next.Stat);

            Assert.True(enumerator.MoveNext());
            var nodes2 = enumerator.Current;
            Assert.NotEmpty(nodes2);

            Assert.True(enumerator.MoveNext());
            var nodes3 = enumerator.Current;
            Assert.NotEmpty(nodes3);

            Assert.True(enumerator.MoveNext());
            var nodes4 = enumerator.Current;
            Assert.NotEmpty(nodes4);

            Assert.True(enumerator.MoveNext());
            var nodes5 = enumerator.Current;
            Assert.NotEmpty(nodes5);
        }

        [Fact]
        public void SoftWakachi()
        {
            var nodes = this.tagger.ParseSoftWakachi("すもももももももものうち", 0.0007f);

            foreach (var node in nodes)
            {
                Assert.InRange(node.Prob, 0f, 1f);
            }

            Assert.NotEmpty(nodes.Where(n => n.Prob > 0f));
            Assert.NotEmpty(nodes.Where(n => n.Prob < 1f));

            var nBestResults = tagger.ParseNBest("すもももももももものうち");
            foreach (var node in nBestResults.Take(10).SelectMany(r => r))
            {
                Assert.Contains(node, nodes, new MeCabNodeComparer());
            }
        }

        public void Dispose()
        {
            this.tagger?.Dispose();
        }

        class MeCabNodeComparer : IEqualityComparer<MeCabNodeSuperBase>
        {
            public bool Equals([AllowNull] MeCabNodeSuperBase x, [AllowNull] MeCabNodeSuperBase y)
            {
                return x.Surface == y.Surface && x.Feature == y.Feature;
            }

            public int GetHashCode([DisallowNull] MeCabNodeSuperBase obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
