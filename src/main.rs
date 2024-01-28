use std::fmt::Debug;

fn main() {
    let mut stream = Stream::new("123 identifier");
    let tokens = stream_tokenize(&mut stream);

    println!("{:#?}", tokens)
}

struct Stream {
    source: String,
    position: usize,
}

impl Stream {
    pub fn new(source: &str) -> Stream {
        Stream {
            source: source.to_string(),
            position: 0,
        }
    }

    pub fn get_current_char(&self) -> char {
        self.source.chars().collect::<Vec<_>>()[self.position]
    }

    pub fn get_next_char(&self) -> Option<char> {
        self.source
            .chars()
            .collect::<Vec<_>>()
            .get(self.position + 1)
            .cloned()
    }

    pub fn next(&mut self) {
        self.position += 1;
    }
}

fn stream_tokenize(stream: &mut Stream) -> Vec<Token> {
    let mut tokens = Vec::new();

    while stream.source.len() < stream.position {
        let token = match (stream.get_current_char(), stream.get_next_char()) {
            ('0'..='9', _) => number_tokenize(stream),
            _ => Some(Token::Illegal),
        };

        if let Some(token) = token {
            tokens.push(token)
        }
        stream.next()
    }

    tokens
}

fn number_tokenize(stream: &mut Stream) -> Option<Token> {
    let mut value = String::new();

    while stream.source.len() < stream.position {
        if !stream.get_current_char().is_ascii_digit() {
            break;
        }

        value.push(stream.get_current_char());

        stream.next();
    }

    Some(Token::Number(value))
}

#[derive(Debug)]
enum Token {
    Number(String),
    Identifier,

    Illegal,
}
