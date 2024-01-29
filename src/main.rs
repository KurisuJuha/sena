mod lexing;
mod parsing;

use lexing::tokenize::tokenize;

use crate::parsing::parse::parse;

fn main() {
    let source = "      123 test_identifier ( )";

    let tokens = tokenize(source);
    let root = parse(source);

    println!("{:#?}", tokens);
    println!("{:#?}", root);
}
