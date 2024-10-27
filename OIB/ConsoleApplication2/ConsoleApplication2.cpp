#include <iostream>
#include <vector>
#include <typeinfo>
#include <unordered_map>
#include <string>
#define watch(x) std::cout << typeid(x).name() << " " << #x << " = " << x << std::endl;

//2
std::vector<std::vector<char>> generateKeyMatrix(const std::string& key, size_t width, size_t height, char startLatter, char finishLatter) {
    std::vector<std::vector<char>> matrix(height, std::vector<char>(width, ' '));
    std::string uniqueKey = "";

    for (char ch : key) {
        if (ch != ' ' && uniqueKey.find(ch) == std::string::npos)
            uniqueKey += ch;
    }

    int row = 0, col = 0;
    for (char ch : uniqueKey) {
        matrix[row][col] = ch;
        col++;
        if (col == width) {
            col = 0;
            row++;
        }
    }

    char currentChar = startLatter;
    for (row = 0; row < height; row++) {
        for (col = 0; col < width; col++) {
            if (matrix[row][col] == ' ') {
                while (uniqueKey.find(currentChar) != std::string::npos ||
                    currentChar == finishLatter) {
                    currentChar++;
                }
                matrix[row][col] = currentChar;
                currentChar++;
            }
        }
    }

    return matrix;
}

void findCoordinates(const std::vector<std::vector<char>>& matrix, char ch, int& row, int& col) {
    for (row = 0; row < 5; row++) {
        for (col = 0; col < 5; col++) {
            if (matrix[row][col] == ch) {
                return;
            }
        }
    }
}

std::string trisEncrypt(const std::string& text, const std::vector<std::vector<char>>& matrix, size_t width) {
    std::string encryptedText = "";

    for (size_t i = 0; i < text.length(); i++) {
        char firstChar = text[i];

        int row1 = 0, col1 = 0;
        findCoordinates(matrix, firstChar, row1, col1);

        encryptedText += matrix[(row1 + 1) % width][col1];
    }

    return encryptedText;
}

//3
std::string playfairEncrypt(const std::string& plaintext, const std::vector<std::vector<char>>& matrix) {
    std::string encryptedText = "";

    for (size_t i = 0; i < plaintext.length(); i += 2) {
        char firstChar = plaintext[i];
        char secondChar = (i + 1 < plaintext.length()) ? plaintext[i + 1] : 'X';

        int row1, col1, row2, col2;
        findCoordinates(matrix, firstChar, row1, col1);
        findCoordinates(matrix, secondChar, row2, col2);

        if (row1 == row2) {
            encryptedText += matrix[row1][(col1 + 1) % 5];
            encryptedText += matrix[row2][(col2 + 1) % 5];
        }
        else if (col1 == col2) {
            encryptedText += matrix[(row1 + 1) % 5][col1];
            encryptedText += matrix[(row2 + 1) % 5][col2];
        }
        else {
            encryptedText += matrix[row1][col2];
            encryptedText += matrix[row2][col1];
        }
    }

    return encryptedText;
}

//4
int mislower(int _C) {
    return (_C >= (char)'а' && _C <= (char)'я') ? _C : 0;
}
int mtolower(int _C) {
    return mislower(_C) > 0 ? _C : (_C + ((char)'я' - (char)'а'));
}

std::string encryptVigenere(const std::string& plaintext, const std::string& key) {
    std::string ciphertext = "";
    int keyLength = key.length();

    for (int i = 0; i < plaintext.length(); i++) {
        char plainChar = plaintext[i];
        char keyChar = key[i % keyLength];

        if ((plainChar >= (char)'а' && plainChar <= (char)'я') ||  (plainChar >= (char)'А' && plainChar <= (char)'Я')) {
            char base = mislower(plainChar) > 0 ? char('а') : char('А');
            char shift = mtolower(keyChar) - char('а');

            char encryptedChar = static_cast<char>((plainChar - base + shift) % ((char)'я' - (char)'а') + base);
            ciphertext += encryptedChar;
        }
        else {
            ciphertext += plainChar;
        }
    }

    return ciphertext;
}



#define METHOD(name) std::cout << std::endl << "========================- " << name << " -========================" << std::endl;

int main(int argc, char* argv[]) {
    setlocale(LC_ALL, "Rus");
        // 1
    //    METHOD("Цезарь");
    //int shift = 4;
    //std::string text_C = "Шутро Александр Сергеевич";
    //std::cout << "Исходный текст: " << text_C << std::endl;
    //for (int i = 0; i < text_C.size(); i++) {
    //    text_C[i] += shift;
    //}
    //std::cout << "Зашифрованный текст: " << text_C << std::endl;
    //for (int i = 0; i < text_C.size(); i++) {
    //    text_C[i] -= shift;
    //}
    //std::cout << "Расшифрованный текст: " << text_C << std::endl;

    //// 2
    //METHOD("Трисемуса");
    //std::string key_T = "KEYWORD";
    //std::vector<std::vector<char>> keyMatrix_T = generateKeyMatrix(key_T, 5, 5, 'A', 'J');
    //for (auto line : keyMatrix_T) {
    //    for (auto element : line) {
    //        std::cout << element;
    //    }
    //    std::cout << std::endl;
    //}

    //std::string plaintext_T = "SHUTRO";
    //std::string encryptedText_T = trisEncrypt(plaintext_T, keyMatrix_T, 5);

    //std::cout << "Исходный текст: " << plaintext_T << std::endl;
    //std::cout << "Зашифрованный текст: " << encryptedText_T << std::endl;

    //// 3
    //METHOD("Плейфейра");
    //std::string key_P = "KEYWORD";
    //std::vector<std::vector<char>> keyMatrix_P = generateKeyMatrix(key_P, 5, 5, 'A', 'J');
    //std::string plaintext_P = "SHUTRO";
    //std::string ciphertext_P = playfairEncrypt(plaintext_P, keyMatrix_P);

    //std::cout << "Исходный текст: " << plaintext_P << std::endl;
    //std::cout << "Зашифрованный текст: " << ciphertext_P << std::endl;

    //// 4
    //METHOD("Вижинера");
    //std::string _key = "Защита";
    //std::string _plaintext = "Шутро Александр Сергеевич";
    //std::string _ciphertext = encryptVigenere(_plaintext, _key);

    //std::cout << "Исходный текст: " << _plaintext << std::endl;
    //std::cout << "Зашифрованный текст: " << _ciphertext << std::endl;


    //3 задание
    std::string text_C = "ие михежцчжшйч сшихуцчб";
    std::cout << "Зашифрованный текст: " << text_C << std::endl;
    std::string text = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    for(int shift = 1; shift < 33; shift++) {
      std::string text_D = text_C;

      for (int i = 0; i < text_D.size(); i++) {
        if (text_D[i] == ' ') {
        }
        else {


          int index_text = 0;
          //нахожу индекс по своему алфавиту
          for (int x = 0; x < text.size(); x++) {
            if (text_D[i] == text[x])
              index_text = x;
          
          }
          //проверка на выход за text.size()

          if (index_text + shift +1 <= text.size()) {
            text_D[i] = text[index_text + shift];
          }
          else {
            text_D[i] = text[index_text + shift - text.size()];
          }


        }
      }
      std::cout << shift << " - " << "Расшифрованный текст: " << text_D << std::endl;
    }
    system("pause");
    return 0;
}