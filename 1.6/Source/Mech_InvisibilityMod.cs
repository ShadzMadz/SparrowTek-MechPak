using HarmonyLib;
using RimWorld;
using Verse;

namespace Mech_Invisibility
{
    public class Mech_InvisibilityMod : Mod
    {
        public Mech_InvisibilityMod(ModContentPack pack) : base(pack)
        {
            new Harmony("Mech_InvisibilityMod").PatchAll();
        }
    }

    public class CompProperties_Invisibility : CompProperties_AbilityEffect
    {
        public IntRange durationTicksRange;
        public HediffDef hediff;
        public CompProperties_Invisibility()
        {
            compClass = typeof(CompAbilityEffect_Invisibility);
        }
    }


    public class CompAbilityEffect_Invisibility : CompAbilityEffect
    {
        public new CompProperties_Invisibility Props => (CompProperties_Invisibility)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Hediff hediff = HediffMaker.MakeHediff(Props.hediff, target.Pawn);
            HediffComp_Disappears hediffComp_Disappears = hediff.TryGetComp<HediffComp_Disappears>();
            if (hediffComp_Disappears != null)
            {
                hediffComp_Disappears.ticksToDisappear = Props.durationTicksRange.RandomInRange;
            }
            target.Pawn.health.AddHediff(hediff);
        }
    }
}
