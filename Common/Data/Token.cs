using System;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Data
{
    public class Token
    {
        public int Amount { get; set; }
        public TokenDenomination Denomination { get;set;}

        public static Result<Token> Create(string input)
        {
            var split = input.Split(' ');
            if (split.Length == 1 ||  int.TryParse(split[0], out var amount))
                return Result.Fail<Token>(input + " is not a valid token input. it should be AMOUNT [Gold|Silver|Bronze].");

            var denomination = TokenDenomination.Bronze;
            switch(split[1].ToLower())
            {
                case "bronze":
                    denomination = TokenDenomination.Bronze;
                    break;
                case "silver":
                    denomination = TokenDenomination.Silver;
                    break;
                case "gold":
                    denomination = TokenDenomination.Gold;
                    break;
                default:
                return Result.Fail<Token>(input + " is not a valid token input. it should be AMOUNT [Gold|Silver|Bronze].");
            }
          
            var newToken = new Token()
            {
                Amount = amount,
                Denomination = denomination,
            };
            return Result.Ok(newToken);
        }
    }
}
