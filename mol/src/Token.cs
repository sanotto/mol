using System;

namespace mol
{
    public enum TokenTypes {    WORD,
                                NUMBER,
                                STRING,
                                LABEL,
                                LINE ,
                                EQUALS,
                                OPERATOR ,
                                FNCTION ,
                                LEFT_PAREN ,
                                RIGHT_PAREN ,
                                COLON,
                                EOF }
	public class Token
	{
        public string      text;
        public TokenTypes  type;
        

		public Token (string _text, TokenTypes _type)
		{
			text = _text.ToLower();
			type = _type;
		}
        public string toString()
        {
            return text+"["+type.ToString()+"]";
        }
	}
}

