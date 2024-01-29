use crate::lexing::token::Token;

pub struct Stream {
    pub source: Vec<Token>,
    pub position: usize,
}

impl Stream {
    pub fn new(source: Vec<Token>) -> Stream {
        Stream {
            source,
            position: 0,
        }
    }

    pub fn get_current_token(&self) -> Token {
        self.source[self.position].clone()
    }

    pub fn get_next_token(&self) -> Option<Token> {
        self.source.get(self.position + 1).cloned()
    }

    pub fn next(&mut self) {
        self.position += 1;
    }
}
