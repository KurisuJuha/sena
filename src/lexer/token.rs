#[derive(Debug)]
pub enum Token {
    Number(String),
    Identifier(String),
    RParen,
    LParen,

    Space(String),

    Illegal(char),
}
