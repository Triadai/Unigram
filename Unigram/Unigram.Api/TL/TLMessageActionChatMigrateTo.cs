// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLMessageActionChatMigrateTo : TLMessageActionBase 
	{
		public Int32 ChannelId { get; set; }

		public TLMessageActionChatMigrateTo() { }
		public TLMessageActionChatMigrateTo(TLBinaryReader from)
		{
			Read(from);
		}

		public override TLType TypeId { get { return TLType.MessageActionChatMigrateTo; } }

		public override void Read(TLBinaryReader from)
		{
			ChannelId = from.ReadInt32();
		}

		public override void Write(TLBinaryWriter to)
		{
			to.Write(0x51BDB021);
			to.Write(ChannelId);
		}
	}
}