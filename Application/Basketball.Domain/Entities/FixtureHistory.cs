using System;

using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// This exists purely for reporting purposes
    /// </summary>
    public class FixtureHistory : Entity
    {
        public virtual int          Fixture_Id       { get; set; }
        public virtual TeamLeague   HomeTeamLeague   { get; set; }
        public virtual TeamLeague   AwayTeamLeague   { get; set; }
        public virtual DateTime     FixtureDate      { get; set; }
        public virtual int?         HomeTeamScore    { get; set; }
        public virtual int?         AwayTeamScore    { get; set; }
        public virtual bool         IsCupFixture     { get; set; }
        public virtual int?         CupRoundNo       { get; set; }
        public virtual Cup          Cup              { get; set; }
        public virtual string       IsPlayed         { get; set; } // TODO Boolean?
        public virtual DateTime?    ResultAddedDate  { get; set; }
        public virtual string       IsCancelled      { get; set; }
        public virtual string       TipOffTime       { get; set; }
        public virtual string       HasPlayerStats   { get; set; }
        public virtual bool         IsPenaltyAllowed { get; set; }
        public virtual Referee      Referee1         { get; set; }
        public virtual Referee      Referee2         { get; set; }
        public virtual DateTime?    LastUpdated      { get; set; }
        public virtual User         LastUpdatedBy    { get; set; }
        public virtual string       Change           { get; set; }
        public virtual bool         IsForfeit        { get; set; }
        public virtual Team         ForfeitingTeam   { get; set; }
    }
}
