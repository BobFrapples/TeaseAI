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
        /// Accepts an answer other than what the sub spoke, works similar to <see cref="DifferentAnswer"/>
        /// <para>Do you like pain?
        /// <para>[yes] Good</para>
        /// <para>[no] Too bad</para>
        /// <para>@AcceptAnswer I guess we'll find out</para>
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

        /// <summary>
        /// This isn't an actual command, but a flag to note a default when trying to handle a user response
        /// <para>Are you sure you want to continue? </para>
        /// <para> [yes] Well...alright then *grin* </para>
        /// <para> [no] No problem, honey</para>
        /// <para> @DifferentAnswer Yes or no?</para>
        /// </summary>
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
        public const string RapidTextOff = @"@RapidTextOff";
        /// <summary>
        /// Set sleep time between messages to zero
        /// </summary>
        public const string RapidCodeOn = @"@RapidCodeOn";
        public const string RapidTextOn = @"@RapidTextOn";

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
        public const string PlaySpecificVideo = @"@PlayVideo(";
        public const string PlaySpecificVideoSquareBrackets = @"@PlayVideo[";

        /// <summary>
        /// <para>Make the sub torture his cock. pause briefly for it to happen (TaskPauseMinimum and TaskPauseMaximum in settings)</para>
        /// <para>files are located in BASE_FOLDER\Scripts\DOMME_PERSONALITY\CBT\CBTCock[_First].txt with _First being called the first time</para>    
        /// </summary>
        public const string CockTorture = @"@CBTCock";

        /// <summary>
        /// <para>Make the sub torture his balls. pause briefly for it to happen (TaskPauseMinimum and TaskPauseMaximum in settings)</para>
        /// <para>files are located in BASE_FOLDER\Scripts\DOMME_PERSONALITY\CBT\CBTBalls[_First].txt with _First being called the first time</para>    
        /// </summary>
        public const string BallTorture = @"@CBTBall";

        /// <summary>
        /// <para>Usage: @CustomTask(TASKNAME)</para>
        /// <para>Create a custom task for the sub, pausing briefly for it to happen (TaskPauseMinimum and TaskPauseMaximum in settings)</para>
        /// <para>Files are located in BASE_FOLDER\Scripts\DOMME_PERSONALITY\Custom\Tasks\TASKNAME[_First].txt with _First being called the first time</para>    
        /// </summary>
        public const string CustomTask = @"@CustomTask(";

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

        /// <summary>
        /// Enables chastity in settings
        /// </summary>
        public const string ChastityOn = @"@ChastityOn";

        /// <summary>
        /// Disables chastity in settings
        /// </summary>
        public const string ChastityOff = @"@ChastityOff";

        /// <summary>
        /// <para>Usage: @AddTokens(AMOUNT [Gold|Silver|Bronze][,AMOUNT[Gold|Silver|Bronze]])</para>
        /// <para>Add AMOUNT of Gold, Silver, or Bronze tokens to the subs purse. This can be repeated multiple times</para>
        /// </summary>
        public const string AddTokens = @"@AddTokens(";

        /// <summary>
        /// Start a new game of Risky Pick
        /// </summary>
        public const string RiskyPickStart = @"@StartRiskyPick";

        /// <summary>
        /// Pauses the script until all risky pick boxes have been chosen for the current round
        /// </summary>
        public static string RiskyPickWaitForCase => @"@ChooseRiskyPick";
        /// <summary>
        /// State the number of edges and trigger the next round of risky pick
        /// </summary>
        [Obsolete("This is not a command and needs to be moved to vocabulary")]
        public static string RiskyPickRespondCase => @"#RP_RespondCase";

        /// <summary>
        /// <para>Make an offer from the Domme at the end of the round</para>
        /// <para>Requires Risky pick be running and the sub has at least chosen their case</para>
        /// </summary>
        public static string RiskyPickCheck => @"@CheckRiskyPick";

        /// <summary>
        /// <para>Usage: @SelectCaseRiskyPick(CaseNumber)</para>
        /// <para>Select the next case for Risky Pick</para>
        /// </summary>
        public static string RiskyPickSelectCase => @"@SelectCaseRiskyPick(";

        /// <summary>
        /// Pauses script execution. Used for code 
        /// </summary>
        public static string Unpause => @"@Unpause";

        /// <summary>
        /// Unpauses script execution. Used for code 
        /// </summary>
        public static string Pause => @"@Unpause";

        /// <summary>
        /// Provides a description of the script, does nothing in the script
        /// </summary>
        public static string Info => @"@Info";

        /// <summary>
        /// flag whatever image is being displaed as liked.
        /// </summary>
        public static string LikeImage => @"@LikeBlogImage";

        /// <summary>
        /// Flag whatever image is being displayed as disliked
        /// </summary>
        public static string DislikeImage => @"@DislikeBlogImage";
    }
}
