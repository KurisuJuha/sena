use crate::lexing::{token::Token, tokenize::tokenize};

use super::stream::Stream;

pub enum Expression {
    Eval(String, Vec<Expression>),
}

#[derive(Debug)]
pub enum Statement {
    Root(Vec<Statement>),
}

pub fn parse(source: &str) -> Statement {
    parse_stream(&mut Stream::new(tokenize(source)))
}

fn parse_stream(stream: &mut Stream) -> Statement {
    let mut statements = Vec::new();

    while stream.source.len() > stream.position {
        let statement = parse_statement(stream);
        if let Some(statement) = statement {
            statements.push(statement);
        } else {
            stream.next();
        }
    }

    Statement::Root(statements)
}

fn parse_statement(stream: &mut Stream) -> Option<Statement> {
    match (stream.get_current_token(), stream.get_next_token()) {
        (Token::Identifier(_), Some(Token::LParen)) => parse_eval(stream),
        _ => {
            println!(
                "not yet implemented {:?}, {:?}",
                stream.get_current_token(),
                stream.get_next_token()
            );
            None
        }
    }
}

fn parse_expression(stream: &mut Stream) -> Option<Expression> {
    
}

fn parse_eval(stream: &mut Stream) -> Option<Statement> {
    if !matches!(stream.get_current_token(), Token::Identifier(_)) {
        return None;
    }

    stream.next();

    if !matches!(stream.get_current_token(), Token::LParen) {
        return None;
    }

    stream.next();



    None
}
