#include <iostream>
#include <string>
#include <cctype>

using namespace std;


// Функция для шифра Цезаря
std::string caesarCipher(const std::string& text, int shift) {
    setlocale(LC_ALL, "russian");
    std::string result = "";
    for (char c : text) {
        if (isalpha(c)) {
            int shiftAmount = shift % 26;
            char base = (islower(c)) ? 'а' : 'А';
            char shifted = base + (c - base + shiftAmount) % 26;
            result += shifted;
        }
        else {
            result += c;
        }
    }
    return result;
}

// Функция для шифра Трисемуса
std::string trisemusCipher(const std::string& text, const std::string& key) {
    setlocale(LC_ALL, "russian");
    std::string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    std::string result = "";
    for (char c : text) {
        if (isalpha(c)) {
            bool isLower = islower(c);
            char charUpper = toupper(c);
            size_t index = key.find(charUpper);
            if (index != std::string::npos) {
                char encryptedChar = (isLower) ? tolower(alphabet[index]) : alphabet[index];
                result += encryptedChar;
            }
            else {
                result += c;
            }
        }
        else {
            result += c;
        }
    }
    return result;
}

// Функция для шифра Вижинера
std::string vigenereCipher(const std::string& text, const std::string& key) {
    setlocale(LC_ALL, "russian");
    std::string result = "";
    size_t keyLength = key.length();
    for (size_t i = 0; i < text.length(); ++i) {
        char c = text[i];
        if (isalpha(c)) {
            bool isLower = islower(c);
            char charUpper = toupper(c);
            char keyChar = toupper(key[i % keyLength]);
            int shift = keyChar - 'А';
            char shifted = charUpper + shift;
            if (shifted > 'Я') {
                shifted -= 26;
            }
            if (isLower) {
                result += tolower(shifted);
            }
            else {
                result += shifted;
            }
        }
        else {
            result += c;
        }
    }
    return result;
}

// Функция для расшифровки шифра Трисемуса
std::string trisemusDecipher(const std::string& text, const std::string& key) {
    setlocale(LC_ALL, "russian");
    std::string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    std::string result = "";
    for (char c : text) {
        if (isalpha(c)) {
            bool isLower = islower(c);
            char charUpper = toupper(c);
            size_t index = key.find(charUpper);
            if (index != std::string::npos) {
                char decryptedChar = (isLower) ? tolower(alphabet[index]) : alphabet[index];
                result += decryptedChar;
            }
        }
        else {
            result += c;
        }
    }
    return result;
}

int main() {
    setlocale(LC_ALL, "russian");
    // Зашифрование сообщения с использованием разных шифров
    std::string message = "Шутро Александр Сергеевич";
    int caesarShift = 19;
    std::string trisemusKey = "АБСТРАКЦИЯ";
    std::string vigenereKey = "ЗАЩИТА";

    std::string caesarEncrypted = caesarCipher(message, caesarShift);
    std::string trisemusEncrypted = trisemusCipher(message, trisemusKey);
    std::string vigenereEncrypted = vigenereCipher(message, vigenereKey);

    std::cout << "Зашифрованное сообщение (шифр Цезаря): " << caesarEncrypted << std::endl;
    std::cout << "Зашифрованное сообщение (шифр Трисемуса): " << trisemusEncrypted << std::endl;
    std::cout << "Зашифрованное сообщение (шифр Вижинера): " << vigenereEncrypted << std::endl;

    // Расшифровка сообщения с использованием шифра Трисемуса
    std::string trisemusEncryptedMessage = "нп тр яч дн ка бо ат дъ ка цр кб щг уф уч тб ты";
    std::string trisemusDecrypted = trisemusDecipher(trisemusEncryptedMessage, trisemusKey);

    std::cout << "Расшифрованное сообщение (шифр Трисемуса): " << trisemusDecrypted << std::endl;

    return 0;
}