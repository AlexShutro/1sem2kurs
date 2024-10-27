#include <iostream>
#include <string>
#include <locale>
using namespace std;

// Шифр Цезаря
string caesarDecrypt(string text, int key) {
    setlocale(LC_ALL, "russian");
    string result = "";
    for (char c : text) {
        if (isalpha(c)) {
            if (isupper(c))
                result += char(int(c - key - 1040) % 32 + 1040);
            else
                result += char(int(c - key - 1072) % 32 + 1072);
        }
        else {
            result += c;
        }
    }
    return result;
}

// Шифр Трисемуса
string trithemiusDecrypt(string text, string key) {
    string result = "";
    int keyLength = key.length();
    int j = 0;
    for (char c : text) {
        if (isalpha(c)) {
            int shift = int(toupper(key[j % keyLength]) - 1040);
            if (isupper(c))
                result += char(int(c - shift - 1040) % 32 + 1040);
            else
                result += char(int(c - shift - 1072) % 32 + 1072);
            j++;
        }
        else {
            result += c;
        }
    }
    return result;
}

// Шифр Плейфейра
string playfairDecrypt(string text, string keyword) {
    setlocale(LC_ALL, "russian");
    string result = "";
    string key = keyword + "абвгдежзийклмнопрстуфхцчшщъыьэюя";
    string plaintext = "";
    for (char c : text) {
        if (isalpha(c, locale("ru_RU.utf8")))
            plaintext += tolower(c, locale("ru_RU.utf8"));
    }
    int plaintextLength = plaintext.length();
    for (int i = 0; i < plaintextLength; i += 2) {
        char a = plaintext[i];
        char b = (i + 1 < plaintextLength) ? plaintext[i + 1] : 'ф';
        int a_pos = key.find(a);
        int b_pos = key.find(b);
        int a_row = a_pos / 8;
        int a_col = a_pos % 8;
        int b_row = b_pos / 8;
        int b_col = b_pos % 8;

        if (a_row == b_row) {
            result += key[a_row * 8 + (a_col - 1 + 8) % 8];
            result += key[b_row * 8 + (b_col - 1 + 8) % 8];
        }
        else if (a_col == b_col) {
            result += key[((a_row - 1 + 8) % 8) * 8 + a_col];
            result += key[((b_row - 1 + 8) % 8) * 8 + b_col];
        }
        else {
            result += key[a_row * 8 + b_col];
            result += key[b_row * 8 + a_col];
        }
    }
    return result;
}

// Шифр Вижинера
string vigenereDecrypt(string text, string keyword) {
    string result = "";
    int keywordLength = keyword.length();
    int j = 0;
    for (char c : text) {
        if (isalpha(c, locale("ru_RU.utf8"))) {
            int shift = int(toupper(keyword[j % keywordLength], locale("ru_RU.utf8")) - 1040);
            if (isupper(c))
                result += char(int(c - shift - 1040) % 32 + 1040);
            else
                result += char(int(c - shift - 1072) % 32 + 1072);
            j++;
        }
        else {
            result += c;
        }
    }
    return result;
}

int main() {
    string message = "Шутро Александр Сергеевич";
    int caesarKey = 3;
    string trithemiusKey = "абстракция";
    string playfairKeyword = "защита";
    string vigenereKeyword = "защита";

    string caesarResult = caesarCipher(message, caesarKey);
    string trithemiusResult = trithemiusCipher(message, trithemiusKey);
    string playfairResult = playfairCipher(message, playfairKeyword);
    string vigenereResult = vigenereCipher(message, vigenereKeyword);

    string decryptedCaesarResult = caesarDecrypt(caesarResult, caesarKey);
    string decryptedTrithemiusResult = trithemiusDecrypt(trithemiusResult, trithemiusKey);
    string decryptedPlayfairResult = playfairDecrypt(playfairResult, playfairKeyword);
    string decryptedVigenereResult = vigenereDecrypt(vigenereResult, vigenereKeyword);

    cout << "Шифр Цезаря: " << caesarResult << endl;
    cout << "Дешифрованный Шифр Цезаря: " << decryptedCaesarResult << endl;
    cout << "Шифр Трисемуса: " << trithemiusResult << endl;
    cout << "Дешифрованный Шифр Трисемуса: " << decryptedTrithemiusResult << endl;
    cout << "Шифр Плейфейра: " << playfairResult << endl;
    cout << "Дешифрованный Шифр Плейфейра: " << decryptedPlayfairResult << endl;
    cout << "Шифр Вижинера: " << vigenereResult << endl;
    cout << "Дешифрованный Шифр Вижинера: " << decryptedVigenereResult << endl;

    return 0;
}