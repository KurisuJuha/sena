#[derive(Debug)]
pub enum Token {
    Number(String),
    Identifier(String),

    Illegal(char),
}
