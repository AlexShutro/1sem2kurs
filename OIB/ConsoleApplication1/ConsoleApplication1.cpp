#include <iostream>
#include <string>
#include <vector>

void swipByPositions(char** table, std::vector<int> positions, int index = 0) {
    if (index >= positions.size())
        return;

    char* buffer = table[positions[index]];
    swipByPositions(table, positions, index + 1);
    table[positions[index]] = buffer;
}

void decryptWithKey(std::string& ciphertext, const std::string& key, int height) {
    size_t width = key.size();
    char** table = new char* [width]();

    for (int x = 0; x < width; x++) {
        table[x] = new char[height];
        for (int y = 0; y < height; y++) {
            if (x + y * width < ciphertext.size())
                table[x][y] = ciphertext[x + y * width];
            else
                table[x][y] = ' ';
        }
    }

    std::vector<int> values = { 0, 0, 0, 0, 0, 0, 0 };
    int counter = 0;
    for (char letter = 'А'; letter <= 'Я'; letter++) {
        for (int i = 0; i < key.size(); i++) {
            if (key[i] == letter && values[i] == 0) {
                values[i] = counter;
                counter++;
            }
        }
    }

    swipByPositions(table, values);

    for (int x = 0; x < width; x++) {
        for (int y = 0; y < height; y++) {
            if (x + y * width < ciphertext.size())
                ciphertext[x * height + y] = table[x][y];
            else
                ciphertext[x * height + y] = ' ';
        }
    }

    // Освобождение памяти после использования table
    for (int x = 0; x < width; x++) {
        delete[] table[x];
    }
    delete[] table;
}

int main() {
    setlocale(LC_ALL, "russ");
    std::string key_TD = "ФЕВРАЛЬ";

    std::string plaintext_TD = "_еоовипи_ _ _ы_о_ввв_тттыыытуоо_ _ _атмтерем_у,сес,б_ _тшт _ычкьиьгттт,л,дь";
    std::cout << "Зашифрованный текст: " << plaintext_TD << std::endl;
    decryptWithKey(plaintext_TD, key_TD, 10);
    std::cout << "Исходный текст: " << plaintext_TD << std::endl;

    return 0;
}