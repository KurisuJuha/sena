mod lexer;

use lexer::tokenize::tokenize;

fn main() {
    let tokens = tokenize("      123 test_identifier ( )");

    println!("{:#?}", tokens)
}
