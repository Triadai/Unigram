// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLPageBlockParagraph : TLPageBlockBase 
	{
		public TLRichTextBase Text { get; set; }

		public TLPageBlockParagraph() { }
		public TLPageBlockParagraph(TLBinaryReader from)
		{
			Read(from);
		}

		public override TLType TypeId { get { return TLType.PageBlockParagraph; } }

		public override void Read(TLBinaryReader from)
		{
			Text = TLFactory.Read<TLRichTextBase>(from);
		}

		public override void Write(TLBinaryWriter to)
		{
			to.Write(0x467A0766);
			to.WriteObject(Text);
		}
	}
}