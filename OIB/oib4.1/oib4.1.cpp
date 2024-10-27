#include <iostream>
#include <string>
#include <algorithm>

std::string trisemusCipherDecrypt(const std::string& encryptedMessage, const std::string& key) {
    // Функция для генерации матрицы ключа
    std::string generateKeyMatrix(const std::string & key);
    {
        std::string keyMatrix = key;
        std::string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Удаление повторяющихся символов из ключа
        keyMatrix.erase(std::unique(keyMatrix.begin(), keyMatrix.end()), keyMatrix.end());

        // Добавление оставшихся символов алфавита в матрицу ключа
        for (char c : alphabet) {
            if (keyMatrix.find(c) == std::string::npos) {
                keyMatrix += c;
            }
        }

        return keyMatrix;
    }

    std::string keyMatrix = generateKeyMatrix(key);
    std::string decryptedMessage;

    // Разделение зашифрованного сообщения на пары символов
    for (int i = 0; i < encryptedMessage.length(); i += 2) {
        char char1 = encryptedMessage[i];
        char char2 = encryptedMessage[i + 1];

        int pos1 = keyMatrix.find(char1);
        int pos2 = keyMatrix.find(char2);

        char decryptedChar1, decryptedChar2;

        if (pos1 % 3 == pos2 % 3) {
            // Символы в одной группе
            decryptedChar1 = keyMatrix[(pos1 + 2) % keyMatrix.length()];
            decryptedChar2 = keyMatrix[(pos2 + 2) % keyMatrix.length()];
        }
        else {
            // Символы в разных группах
            int row1 = pos1 / 3;
            int row2 = pos2 / 3;

            int col1 = pos1 % 3;
            int col2 = pos2 % 3;

            decryptedChar1 = keyMatrix[row1 * 3 + col2];
            decryptedChar2 = keyMatrix[row2 * 3 + col1];
        }

        decryptedMessage += decryptedChar1;
        decryptedMessage += decryptedChar2;
    }

    return decryptedMessage;
}

int main() {
    std::string encryptedMessage = "ъгчгл кыпргл бгнщзг";
    std::string key = "ПРАВИТЕЛЬ";

    std::string decryptedMessage = trisemusCipherDecrypt(encryptedMessage, key);
    std::cout << decryptedMessage << std::endl;

    return 0;
}