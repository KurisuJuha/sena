pub struct Stream {
    pub source: String,
    pub position: usize,
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
