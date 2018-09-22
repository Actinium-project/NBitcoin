using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using NBitcoin.RPC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace NBitcoin.Altcoins
{

	public class Actinium : NetworkSetBase
	{

		public static Actinium Instance { get; } = new Actinium();

		public override string CryptoCode => "ACM";

		private Actinium()
		{

		}

        //Format visual studio
        //{({.*?}), (.*?)}
        //Tuple.Create(new byte[]$1, $2)
        static Tuple<byte[], int>[] pnSeed6_main = {
    Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0xd9,0xa3,0x17,0x16}, 4334),
    Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x8c,0x52,0x24,0x26}, 4334),
    Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x2d,0x3f,0x5c,0xd2}, 4334),
    Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x68,0x9c,0xe7,0x2e}, 4334),
    Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x4f,0x8f,0xb4,0x39}, 4334),
    Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0xc7,0xf7,0x14,0x54}, 4334),
    Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x59,0x16,0x6e,0xba}, 4334)
    
};
        static Tuple<byte[], int>[] pnSeed6_test = {
    Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0xd9,0xa3,0x17,0x16}, 4335),
    Tuple.Create(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xff,0xff,0x2d,0x4d,0x35,0xfa}, 4335),
};

#pragma warning disable CS0618 // Type or member is obsolete
        public class ActiniumConsensusFactory : ConsensusFactory
		{
			private ActiniumConsensusFactory()
			{
			}

			public static ActiniumConsensusFactory Instance { get; } = new ActiniumConsensusFactory();

			public override BlockHeader CreateBlockHeader()
			{
				return new ActiniumBlockHeader();
			}
			public override Block CreateBlock()
			{
				return new ActiniumBlock(new ActiniumBlockHeader());
			}
		}

		public class ActiniumBlockHeader : BlockHeader
		{
            public override uint256 GetPoWHash()
            {
                //TODO: Implement here. Needs a native C# Lyra2z implementation.
                throw new NotSupportedException();
            }
        }

        public class ActiniumBlock : Block
        {
            public ActiniumBlock(ActiniumBlockHeader header) : base(header)
            {

            }
            public override ConsensusFactory GetConsensusFactory()
            {
                return ActiniumConsensusFactory.Instance;
            }
        }


#pragma warning restore CS0618 // Type or member is obsolete

        protected override void PostInit()
        {
            RegisterDefaultCookiePath("Actinium", new FolderName() { TestnetFolder = "testnet" });
        }

        protected override NetworkBuilder CreateMainnet()
        {
            var builder = new NetworkBuilder();
            builder.SetConsensus(new Consensus()
            {
				SubsidyHalvingInterval = 840000,
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,
				BIP34Hash = new uint256("f1d584601e77187e22daa8d551d8307295474a49a54055a0e3feb182223da7ee"),
				PowLimit = new Target(new uint256("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(3.5 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(2.5 * 60),
				PowAllowMinDifficultyBlocks = false,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 6048,
				MinerConfirmationWindow = 8064,
				CoinbaseMaturity = 100,
				ConsensusFactory = ActiniumConsensusFactory.Instance,
				SupportSegwit = true
			})
            .SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 53 })
            .SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 5 })
            .SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 181 })
            .SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x88, 0xBC, 0x26 })
            .SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x88, 0xDA, 0xEE })
            .SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("acm"))
            .SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("acm"))
            .SetMagic(0xd7b7c1fa)
            .SetPort(4334)
            .SetRPCPort(2300)
            .SetName("acm-main")
            .AddAlias("acm-mainnet")
            .AddAlias("actinium-mainnet")
			.AddAlias("actinium-main")
            .AddSeeds(ToSeed(pnSeed6_main))
            .SetGenesis("01000000000000000000000000000000000000000000000000000000000000000000000091912cefda3a88139528d6a4e6137c6812d9f46df80db48cf8ad222f0eb155ecf14ee05af0ff0f1e62580b000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff5004ffff001d0104484e592054696d65732032342f4170722f3230313820546f726f6e746f2056616e2041747461636b20537573706563742045787072657373656420416e67657220617420576f6d656effffffff0100f2052a01000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
            return builder;
        }


        protected override NetworkBuilder CreateTestnet()
        {
            var builder = new NetworkBuilder();
            builder.SetConsensus(new Consensus()
            {
                SubsidyHalvingInterval = 840000,
                MajorityEnforceBlockUpgrade = 51,
                MajorityRejectBlockOutdated = 75,
                MajorityWindow = 1000,
                PowLimit = new Target(new uint256("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
                PowTargetTimespan = TimeSpan.FromSeconds(3.5 * 24 * 60 * 60),
                PowTargetSpacing = TimeSpan.FromSeconds(2.5 * 60),
                PowAllowMinDifficultyBlocks = true,
                PowNoRetargeting = false,
                RuleChangeActivationThreshold = 375,
                MinerConfirmationWindow = 2016,
                CoinbaseMaturity = 100,
                ConsensusFactory = ActiniumConsensusFactory.Instance
            })
            .SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 43 })
            .SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
            .SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 171 })
            .SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
            .SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
            .SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tacm"))
            .SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tacm"))
            .SetMagic(0xf7c7d2f2)
            .SetPort(4335)
            .SetRPCPort(2301)
            .SetName("acm-test")
            .AddAlias("acm-testnet")
            .AddAlias("actinium-test")
            .AddAlias("actinium-testnet")
            .AddSeeds(ToSeed(pnSeed6_test))
            .SetGenesis("01000000000000000000000000000000000000000000000000000000000000000000000091912cefda3a88139528d6a4e6137c6812d9f46df80db48cf8ad222f0eb155ec1150e05af0ff0f1ec14d00000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff5004ffff001d0104484e592054696d65732032342f4170722f3230313820546f726f6e746f2056616e2041747461636b20537573706563742045787072657373656420416e67657220617420576f6d656effffffff0100f2052a01000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
            return builder;
        }

        protected override NetworkBuilder CreateRegtest()
        {
            var builder = new NetworkBuilder();
            builder.SetConsensus(new Consensus()
            {
                SubsidyHalvingInterval = 150,
                MajorityEnforceBlockUpgrade = 51,
                MajorityRejectBlockOutdated = 75,
                MajorityWindow = 1000,
                PowLimit = new Target(new uint256("7fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
                PowTargetTimespan = TimeSpan.FromSeconds(3.5 * 24 * 60 * 60),
                PowTargetSpacing = TimeSpan.FromSeconds(2.5 * 60),
                PowAllowMinDifficultyBlocks = true,
                MinimumChainWork = uint256.Zero,
                PowNoRetargeting = true,
                RuleChangeActivationThreshold = 108,
                MinerConfirmationWindow = 144,
                CoinbaseMaturity = 100,
                ConsensusFactory = ActiniumConsensusFactory.Instance
            })
            .SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 40 })
            .SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 193 })
            .SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 168 })
            .SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
            .SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
            .SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("racm"))
            .SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("racm"))
            .SetMagic(0xd7b7b3fa)
            .SetPort(14444)
            .SetRPCPort(12300)
            .SetName("acm-reg")
            .AddAlias("acm-regtest")
            .AddAlias("actinium-reg")
            .AddAlias("actinium-regtest")
            .AddDNSSeeds(new DNSSeedData[0])
            .AddSeeds(new NetworkAddress[0])
            .SetGenesis("01000000000000000000000000000000000000000000000000000000000000000000000091912cefda3a88139528d6a4e6137c6812d9f46df80db48cf8ad222f0eb155ec2c50e05af0ff0f1e03c60b000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff5004ffff001d0104484e592054696d65732032342f4170722f3230313820546f726f6e746f2056616e2041747461636b20537573706563742045787072657373656420416e67657220617420576f6d656effffffff0100f2052a01000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
            return builder;
        }
    }
}
