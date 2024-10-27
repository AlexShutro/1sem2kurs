#include <iostream>
#include <random>
#include <ctime>

// Функция для проверки, является ли число простым
bool isPrime(int n) {
    if (n <= 1) return false;
    if (n == 2) return true;
    if (n % 2 == 0) return false;

    for (int i = 3; i * i <= n; i += 2) {
        if (n % i == 0) return false;
    }

    return true;
}

// Функция для генерации случайного простого числа
int generateRandomPrime() {
    std::random_device rd;
    std::default_random_engine gen(rd());
    std::uniform_int_distribution<int> dist(2, 100);

    int candidate;
    do {
        candidate = dist(gen);
    } while (!isPrime(candidate));
    return candidate;
}

// Функция для нахождения НОД
int gcd(int a, int b) {
    while (b != 0) {
        int temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

// Функция для нахождения обратного по модулю
int modInverse(int a, int m) {
    for (int x = 1; x < m; x++) {
        if ((a * x) % m == 1) {
            return x;
        }
    }
    return -1;
}

// Функция для генерации ключей RSA
void generateKeys(int& publicKey, int& privateKey, int& n, int& phi) {
    // Генерация случайных простых чисел p и q
    int p, q;
    do {
        p = generateRandomPrime();
        q = generateRandomPrime();
    } while (p == q);

    n = p * q;
    phi = (p - 1) * (q - 1);

    // Выбор открытой экспоненты
    std::default_random_engine gen(std::random_device{}());
    std::uniform_int_distribution<int> dist(2, phi - 1);

    do {
        publicKey = dist(gen);
    } while (gcd(publicKey, phi) != 1);

    // Вычисление закрытой экспоненты
    privateKey = modInverse(publicKey, phi);
    std::cout << std::endl;
    std::cout << "Генерация случайных простых чисел p и q: " << p << " " << q << std::endl;
    std::cout << "phi(n): " << phi << std::endl;
    std::cout << std::endl;
    std::cout << "publicKey: " << publicKey << std::endl;
    std::cout << "privateKey: " << privateKey << std::endl;
    std::cout << std::endl;

}

// Функция для вычисления хеша сообщения
int improvedHash(const std::string& message, int n) {
    int hash = 0;
    for (char c : message) {
        hash = (static_cast<long long>(hash) * 256 + c) % n;
    }
    std::cout << "Функция для вычисления хеша сообщения hash: " << hash << std::endl;
    return hash;
}

// Функция для вычисления степени по модулю (a^b mod m)
int powerMod(int base, int exponent, int modulus) {
    int result = 1;
    while (exponent > 0) {
        if (exponent % 2 == 1) {
            result = static_cast<long long>(result) * base % modulus;
        }
        base = static_cast<long long>(base) * base % modulus;
        exponent /= 2;
    }
    std::cout << std::endl;
    std::cout << "Функция для вычисления степени по модулю (a^b mod m) result: " << result << std::endl;
    return result;
}

// Функция для создания ЭЦП
int createSignature(int hash, int privateKey, int n) {
    return powerMod(hash, privateKey, n);
}

// Функция для проверки ЭЦП
bool verifySignature(int signature, int hash, int publicKey, int n) {
    int computedHash = powerMod(signature, publicKey, n);
    return computedHash == hash;
}

int main() {
    setlocale(LC_ALL, "Russian");

    int phi = 0;
    // Сообщение для подписи
    std::string message = "Hello, World!";

    // Генерация ключей
    int publicKey, privateKey, n;
    generateKeys(publicKey, privateKey, n, phi);

    // Вычисление хеша сообщения 
    int hash = improvedHash(message, n);

    // Создание ЭЦП
    int signature = createSignature(hash, privateKey, n);

    // Вывод информации
    std::cout << "Original Message: " << message << std::endl;
    std::cout << "Generated Signature: " << signature << std::endl;

    // Проверка ЭЦП
    bool isValid = verifySignature(signature, hash, publicKey, n);
    std::cout << "signature" << " = " << "hash" << " ^ " << "privateKey" << " mod " << "phi(n)" << std::endl;
    std::cout << signature << " = " << hash << " ^ " << privateKey << " mod " << phi << std::endl;
    // Вывод результата проверки
    if (isValid) {
        std::cout << "Signature is valid!" << std::endl;
    }
    else {
        std::cout << "Signature is not valid!" << std::endl;
    }

    return 0;
}
