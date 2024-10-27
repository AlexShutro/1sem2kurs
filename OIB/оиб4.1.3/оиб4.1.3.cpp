#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <locale>

std::string decryptPlayfair(const std::string& message, const std::string& key) {
    std::setlocale(LC_ALL, "Russian");
    const int SIZE = 5; // Размер квадратной таблицы (5x5)
    std::vector<std::vector<char>> table(SIZE, std::vector<char>(SIZE)); // Квадратная таблица

    bool used[32] = { false }; // Использованные буквы алфавита

    // Удаляем повторяющиеся символы из ключа
    std::string unique_key;
    for (char c : key) {
        if (!used[std::toupper(c) - 'А'] && std::isalpha(c)) {
            unique_key += std::toupper(c);
            used[std::toupper(c) - 'А'] = true;
        }
    }

    // Заполняем таблицу ключом
    int row = 0, col = 0;
    for (char c : unique_key) {
        table[row][col] = c;
        if (++col == SIZE) {
            ++row;
            col = 0;
        }
    }

    // Заполняем оставшуюся часть таблицы алфавитом
    for (char c = 'А'; c <= 'Я'; ++c) {
        if (!used[c - 'А']) {
            table[row][col] = c;
            if (++col == SIZE) {
                ++row;
                col = 0;
            }
        }
    }

    // Расшифровываем сообщение
    std::string decrypted_message;
    for (std::size_t i = 0; i < message.length(); i += 2) {
        char a = message[i];
        char b = message[i + 1];

        int a_row = 0, a_col = 0, b_row = 0, b_col = 0;
        for (int row = 0; row < SIZE; ++row) {
            for (int col = 0; col < SIZE; ++col) {
                if (table[row][col] == a) {
                    a_row = row;
                    a_col = col;
                }
                if (table[row][col] == b) {
                    b_row = row;
                    b_col = col;
                }
            }
        }

        if (a_row == b_row) { // Подстановка по горизонтали
            decrypted_message += table[a_row][(a_col + 1) % SIZE];
            decrypted_message += table[b_row][(b_col + 1) % SIZE];
        }
        else if (a_col == b_col) { // Подстановка по вертикали
            decrypted_message += table[(a_row + SIZE - 1) % SIZE][a_col];
            decrypted_message += table[(b_row + SIZE - 1) % SIZE][b_col];
        }
        else { // Прямоугольник
            decrypted_message += table[a_row][b_col];
            decrypted_message += table[b_row][a_col];
        }
    }

    return decrypted_message;
}

int main() {
    std::string message = "НПТРЯЧДНКАБОАТДЪКАЦРКБЩГУФУЧТБТЫ";
    std::string key = "АБСТРАКЦИЯ";
    std::string decrypted_message = decryptPlayfair(message, key);
std::cout << "Расшифрованное сообщение: " << decrypted_message << std::endl;

return 0;
}