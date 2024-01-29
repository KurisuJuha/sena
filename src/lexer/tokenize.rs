use super::{stream::Stream, token::Token};

pub fn tokenize(source: &str) -> Vec<Token> {
    stream_tokenize(&mut Stream::new(source))
}

fn stream_tokenize(stream: &mut Stream) -> Vec<Token> {
    let mut tokens = Vec::new();

    while stream.source.len() > stream.position {
        lexeme(stream);

        let token = match (stream.get_current_char(), stream.get_next_char()) {
            ('0'..='9', _) => number_tokenize(stream),
            ('a'..='z' | 'A'..='Z' | '_', _) => identifier_tokenize(stream),
            _ => illegal_tokenize(stream),
        };

        if let Some(token) = token {
            tokens.push(token)
        } else {
            stream.next()
        }
    }

    tokens
}

fn number_tokenize(stream: &mut Stream) -> Option<Token> {
    let mut value = String::new();

    while stream.source.len() > stream.position {
        if !stream.get_current_char().is_ascii_digit() {
            break;
        }

        value.push(stream.get_current_char());

        stream.next();
    }

    Some(Token::Number(value))
}

fn identifier_tokenize(stream: &mut Stream) -> Option<Token> {
    let mut value = String::new();

    while stream.source.len() > stream.position {
        if !is_identifier_char(stream.get_current_char()) {
            break;
        }

        value.push(stream.get_current_char());
        stream.next();
    }

    Some(Token::Identifier(value))
}

fn is_identifier_char(c: char) -> bool {
    matches!(c, 'a'..='z' | 'A'..='Z' | '_')
}

fn illegal_tokenize(stream: &mut Stream) -> Option<Token> {
    let value = Some(Token::Illegal(stream.get_current_char()));
    stream.next();

    value
}

fn lexeme(stream: &mut Stream) {
    while stream.source.len() > stream.position
        && matches!(stream.get_current_char(), ' ' | '\t' | '\r' | '\n')
    {
        stream.next();
    }
}
