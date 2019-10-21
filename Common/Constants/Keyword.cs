using System;

namespace TeaseAI.Common.Constants
{
    /// <summary>
    /// Command keywords used in the program @End, @Start, etc.
    /// All commands  listed here should start with '@' in a script.
    /// </summary>
    public class Keyword
    {
        /// <summary>
        /// Accepts an answer other than what the sub spoke.
        /// i.e:
        /// Do you like pain?
        /// [yes] Good
        /// [no] Too bad
        /// @AcceptAnswer I guess we'll find out
        /// </summary>
        public const string AcceptAnswer = @"@AcceptAnswer";

        /// <summary>
        /// Allows matching on specific values of AllowsOrgasm, Files use @AllowsOrgasm(XXXX) where XXXX is a value listed in <see cref="AllowsOrgasms"/>
        /// This allows for language tailored specifically to if it can happen
        /// </summary>
        public const string AllowsOrgasm = @"@AllowsOrgasm(";

        /// <summary>
        /// Allows matching on specific values of ApathyLevel, Files use @ApathyLevel(XXXX) where XXXX is a value listed in <see cref="ApathyLevel"/>
        /// This allows for language tailored specifically to if it can happen
        /// </summary>
        public const string ApathyLevel = @"@ApathyLevel(";

        /// <summary>
        /// Matches on ApathyLevelX where X is a number from 1 - 5
        /// </summary>
        public const string ApathyLevelNum = @"@ApathyLevel";

        /// <summary>
        /// This command will go to specified bookmark 50% of the time.
        /// </summary>
        public const string Chance = @"@Chance";

        /// <summary>
        /// Check to see if a flag exists.
        /// @CheckFlag(FlagName) will go to bookmark (FlagName) if it exists.
        /// @CheckFlag(FlagName, Bookmark Name) will go to bookmark name if FlagName exists
        /// </summary>
        public const string CheckFlag = @"@CheckFlag(";

        /// <summary>
        /// This to mark matching lines when the Crazy checkbox is select in settings
        /// </summary>
        public const string Crazy = @"@Crazy";

        public const string Degrading = @"@Degrading";

        public const string DeleteFlag = @"@DeleteFlag(";

        public const string DifferentAnswer = @"@DifferentAnswer";

        /// <summary>
        /// This tease is ended. I believe it is used as a flag for sub-left early vs the tease was completed
        /// </summary>
        public const string EndTease = @"@EndTease";

        /// <summary>
        /// This module is over
        /// </summary>
        public const string End = @"@End";

        /// <summary>
        /// used to denote glitter commands. 
        /// Usage is @Glitter(FLAG). the program will look for FLAG in Apps\Glitter\Script for the personality
        /// </summary>
        public const string Glitter = @"@Glitter(";

        /// <summary>
        /// Command telling the script to move to a specific bookmark. Should only have one per line
        /// Specifying multiple targets will result in a random target being chosen
        /// Ex:
        ///     @Goto(bookmark 1)
        ///     @Goto(random 1, random2, random-3)
        /// </summary>
        public const string Goto = @"@Goto(";

        /// <summary>
        /// The Domme has refused to accept this answer for a question, so the script stays at the current location
        /// </summary>
        public const string LoopAnswer = @"@LoopAnswer";

        /// <summary>
        /// Unsure what the intent of this command is meant to do, current implementation simply removes it.
        /// </summary>
        public const string NullResponse = @"@NullResponse";

        /// <summary>
        /// Sub will be allowed an orgasm
        /// </summary>
        public const string OrgasmAllow = @"@OrgasmAllow";

        /// <summary>
        /// Sub will be denied an orgasm
        /// </summary>
        public const string OrgasmDeny = @"@OrgasmDeny";

        /// <summary>
        /// Sub will be ruining their orgasm
        /// </summary>
        public const string OrgasmRuin = @"@OrgasmRuin";

        /// <summary>
        /// Allows matching on specific values of RuinsOrgasm, Files use @RuinsOrgasm(XXXX) where XXXX is a value listed in <see cref="RuinsOrgasms"/>
        /// This allows for language tailored specifically to if it can happen
        /// </summary>
        public const string RuinsOrgasm = @"@RuinsOrgasm(";

        public const string Sadistic = @"@Sadistic";
        public const string SetFlag = @"@SetFlag(";
        public const string SetTempFlag = @"@TempFlag(";

        public const string Supremacist = @"@Supremacist";

        public const string Vulgar = @"@Vulgar";

        /// <summary>
        /// Command to wait X time.
        /// ex: @Wait(10) will wait 10 seconds
        ///   @Wait(1M) will wait 1 minute
        ///   @Wait(1H) will wait 1 hour 
        /// </summary>
        public const string Wait = @"@Wait(";
        /// <summary>
        /// Increase the chance Sub will get to orgasm
        /// </summary>
        public const string IncreaseOrgasmChance = @"@IncreaseOrgasmChance";

        public const string DecreaseOrgasmChance = @"@DecreaseOrgasmChance";
        public const string IncreaseRuinChance = @"@IncreaseRuinChance";
        public const string DecreaseRuinChance = @"@DecreaseRuinChance";

        /// <summary>
        /// Finds the bookmark matching the frequency that the domme lets the sub orgasm.
        /// i.e. (Never Allows), (Often Allows), etc.
        /// </summary>
        public const string GotoDommeOrgasm = @"@GotoDommeOrgasm";
        public const string GotoDommeRuin = @"@GotoDommeRuin";
        public const string GotoDommeApathy = @"@GotoDommeApathy";
        public const string GotoDommeLevel = @"@GotoDommeLevel";

        /// <summary>
        /// Randomly picks a string from within the text options.
        /// i.e. @RandomText(Hello, Hiya) has a roughly 50/50 chance of using either word
        /// </summary>
        public const string RandomText = @"@RandomText(";
        /// <summary>
        /// Set sleep time between messages to default value
        /// </summary>
        public const string RapidCodeOff = @"@RapidCodeOff";
        /// <summary>
        /// Set sleep time between messages to zero
        /// </summary>
        public const string RapidCodeOn = @"@RapidCodeOn";

        /// <summary>
        /// Alias for <see cref="RandomText"/> 
        /// </summary>
        public const string RT = @"@RT(";

        public const string ShowButtImage = @"@ShowButtImage";
        public const string ShowButtsImage = @"@ShowButtsImage";
        /// <summary>
        /// Show a random image local or blog
        /// </summary>
        public const string ShowImage = @"@ShowImage";
        /// <summary>
        /// Show a random local image.
        /// </summary>
        public const string ShowLocalImage = @"@ShowLocalImage";
        /// <summary>
        /// Show a local image in a category listed.
        /// i.e. @ShowLocalImage(butt,femdom)
        /// </summary>
        public const string ShowLocalCategoryImage = @"@ShowLocalImage(";

        /// <summary>
        /// Show a random blog image
        /// </summary>
        public const string SearchImageBlog = @"@SearchImageBlog";

        /// <summary>
        /// alias of <see cref="SearchImageBlog"/> 
        /// </summary>
        public const string SearchImageBlogAgain = @"@SearchImageBlogAgain";

        public const string ShowBoobImage = @"@ShowBoobImage";
        public const string ShowBoobsImage = @"@ShowBoobsImage";

        public const string ShowHardcoreImage = @"@ShowHardcoreImage";
        public const string ShowSoftcoreImage = @"@ShowSoftcoreImage";
        public const string ShowLesbianImage = @"@ShowLesbianImage";
        public const string ShowBlowjobImage = @"@ShowBlowjobImage";
        public const string ShowFemdomImage = @"@ShowFemdomImage";
        public const string ShowLezdomImage = @"@ShowLezdomImage";
        public const string ShowHentaiImage = @"@ShowHentaiImage";
        public const string ShowGayImage = @"@ShowGayImage";
        public const string ShowMaledomImage = @"@ShowMaledomImage";
        public const string ShowCaptionsImage = @"@ShowCaptionsImage";
        public const string ShowGeneralImage = @"@ShowGeneralImage";
        public const string ShowLikedImage = @"@ShowLikedImage";
        public const string ShowDislikedImage = @"@ShowDislikedImage";
        public const string ShowBlogImage = @"@ShowBlogImage";
        /// <summary>
        /// This command is deprecated. Please use @ShowBlogImage instead
        /// </summary>
        [Obsolete("Please use ShowBlogImage instead")]
        public const string NewBlogImage = @"@NewBlogImage";

        /// <summary>
        /// Plays a random video *not* in JOI or CH genre
        /// </summary>
        public static string PlayVideo = @"@PlayVideo";
        /// <summary>
        /// specifically play a Jerk Off Instruction Video
        /// </summary>
        public const string PlayJoiVideo = @"@PlayJOIVideo";
        public const  string PlaySpecificVideo = @"@PlayVideo(";
        public const  string PlaySpecificVideoSquareBrackets = @"@PlayVideo[";
        public const  string CockTorture = @"@CBTCock";

        /// <summary>
        /// When used in conjunction with a PlayVideo command, it will randomize the starting location,
        /// When used stand alone, it will randomly move the currently playing video to a new location.
        /// Ignored if no video is playing
        /// </summary>
        public const string JumpVideo = @"@JumpVideo";

        /// <summary>
        /// Tell the sub to start stroking. also triggers taunts
        /// </summary>
        public const string StartStroking = @"@StartStroking";

        /// <summary>
        /// Process this line *only* if the specified flag is not set.
        /// </summary>
        public const string NotFlag = @"@NotFlag(";

        /// <summary>
        /// Process this line *only* if the specified flag is set
        /// </summary>
        public const string Flag = @"@Flag(";

        /// <summary>
        /// Call another script, if a second parameter is passed, will move directly to that bookmark in the new script
        /// </summary>
        public const string Call = @"@Call(";

        public const string Edge = @"@Edge";

        /// <summary>
        /// Used to filter out lines in vocabulary files that have the LongEdge keyword
        /// </summary>
        public const string LongEdgeFilter = @"@LongEdge";

        /// <summary>
        /// Filter used when the sub's cock is considered big
        /// </summary>
        public const string BigCockFilter = @"@CockLarge";

        /// <summary>
        /// Filter used when the sub's cock is considered small
        /// </summary>
        public const string SmallCockFilter = @"@CockSmall";

        public const string SubOldFilter = @"@SubOld";
        public const string SubYoungFilter = @"@SubYoung";
        public const string SelfOldFilter = @"@SelfOld";
        public const string SelfYoungFilter = @"@SelfYoung";
        public const string SubInChastity = @"@InChastity";

        public const string CupSizeA = @"@ACup";
        public const string CupSizeB = @"@BCup";
        public const string CupSizeC = @"@CCup";
        public const string CupSizeD = @"@DCup";
        public const string CupSizeDD = @"@DDCup";
        public const string CupSizeDDD = @"@DDD+Cup";

        /// <summary>
        /// <code>@SetVar[png_know_you_better_questions_count]=[0]</code>
        /// </summary>
        public const string SetVar = @"@SetVar";

        /// <summary>
        /// Set the domme away from keyboard. 
        /// </summary>
        public const string AfkOn = @"@AFKOn";

        /// <summary>
        /// Set the domme back at keyboard
        /// </summary>
        public const string AfkOff = @"@AFKOff";

        /// <summary>
        /// Porn isn't allowed
        /// </summary>
        public const string PornAllowedOff = @"@PornAllowedOff";

        /// <summary>
        /// Porn is allowed
        /// </summary>
        public const string PornAllowedOn = @"@PornAllowedOn";
    }
}
