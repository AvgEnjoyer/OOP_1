grammar LabCalculator;
/*
Parser Rules
*/
compileUnit : expression EOF;
expression:
        LPAREN expression RPAREN #PathenthesizedExpr
        |SIN LPAREN expression RPAREN #SinExpr
        |expression EXPONENT expression #ExponentialExpr
        |expression operatorToken =(MULTIPLY|DIVIDE) expression #MultiplicativeExpr
        |expression operatorToken =(ADD|SUBTRACT) expression #AdditiveExpr 
        |MAX LPAREN expression COMMA expression RPAREN #MaxExpr
        |MIN LPAREN expression COMMA expression RPAREN #MinExpr
        |NUMBER #NumberExpr
        |NUMBER #IdentifierExpr
        
        ;
        /* 
        *LexerRules
        */
        SIN: 'sin';
        NUMBER: INT ('.'INT)?;
        IDENTIFIER: [a-zA-Z]+[1-9][0-9];
        INT: ('0'..'9')+;
        EXPONENT: '^';
        MULTIPLY: '*';
        DIVIDE: '/';
        SUBTRACT: '-';
        ADD: '+';
        LPAREN: '(';
        RPAREN: ')';
        COMMA: ',';
        MIN: 'min';
        MAX: 'max';
        WS:[\t\r\n]->channel(HIDDEN);