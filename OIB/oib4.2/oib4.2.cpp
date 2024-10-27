#include <iostream>
#include <cmath>

// Функция для нахождения НОД двух чисел
int gcd(int a, int b) {
    if (b == 0) {
        return a;
    }
    return gcd(b, a % b);
}

// Функция для вычисления обратного элемента по модулю
int mod_inverse(int a, int m) {
    for (int x = 1; x < m; x++) {
        if ((a * x) % m == 1) {
            return x;
        }
    }
    return -1; // обратного элемента не существует
}

// Функция для вычисления (base^exponent) % modulus
int mod_pow(int base, int exponent, int modulus) {
    int result = 1;
    base = base % modulus;

    while (exponent > 0) {
        if (exponent % 2 == 1) {
            result = (result * base) % modulus;
        }
        base = (base * base) % modulus;
        exponent /= 2;
    }

    return result;
}

// Алгоритм RSA

struct RSAKeys {
    int n; // модуль
    int e; // открытая экспонента
    int d; // закрытая экспонента
};

RSAKeys generateRSAKeys(int p, int q) {
    RSAKeys keys;
    keys.n = p * q;
    int phi = (p - 1) * (q - 1);

    // Выбор открытой экспоненты e (обычно фиксированное значение, например, 65537)
    keys.e = 65537;

    // Вычисление закрытой экспоненты d
    keys.d = mod_inverse(keys.e, phi);

    return keys;
}

int rsa_encrypt(int plaintext, int e, int n) {
    return mod_pow(plaintext, e, n);
}

int rsa_decrypt(int ciphertext, int d, int n) {
    return mod_pow(ciphertext, d, n);
}

// Алгоритм Диффи-Хеллмана

int diffie_hellman(int p, int g, int private_key) {
    return mod_pow(g, private_key, p);
}

// Алгоритм Эль-Гамаля

struct ElGamalKeys {
    int p; // большое простое число
    int g; // примитивный корень по модулю p
    int y; // открытый ключ (g^x mod p)
    int x; // закрытый ключ
};

ElGamalKeys generateElGamalKeys(int p, int g, int x) {
    ElGamalKeys keys;
    keys.p = p;
    keys.g = g;
    keys.x = x;
    keys.y = mod_pow(g, x, p);
    return keys;
}

struct ElGamalCiphertext {
    int a;
    int b;
};

ElGamalCiphertext elgamal_encrypt(int plaintext, int p, int g, int y) {
    int k = rand() % (p - 2) + 1; // случайное k, 1 <= k <= p-2
    int a = mod_pow(g, k, p);
    int b = (mod_pow(y, k, p) * plaintext) % p;
    return { a, b };
}

int elgamal_decrypt(ElGamalCiphertext ciphertext, int p, int x) {
    int a = ciphertext.a;
    int b = ciphertext.b;
    int s = mod_pow(a, x, p);
    int plaintext = (b * mod_inverse(s, p)) % p;
    return plaintext;
}

int main() {
    // RSA
    int p = 91;
    int q = 63;
    RSAKeys rsaKeys = generateRSAKeys(p, q);
    int plaintext_rsa = 42;
    int encrypted_rsa = rsa_encrypt(plaintext_rsa, rsaKeys.e, rsaKeys.n);
    int decrypted_rsa = rsa_decrypt(encrypted_rsa, rsaKeys.d, rsaKeys.n);

    std::cout << "RSA:" << std::endl;
    std::cout << "Original: " << plaintext_rsa << std::endl;
    std::cout << "Encrypted: " << encrypted_rsa << std::endl;
    std::cout << "Decrypted: " << decrypted_rsa << std::endl;

    // Диффи-Хеллман
    int dh_p = 33;
    int dh_g = 10;
    int dh_privateAlice = 6;
    int dh_privateBob = 15;
    int dh_publicAlice = diffie_hellman(dh_p, dh_g, dh_privateAlice);
    int dh_publicBob = diffie_hellman(dh_p, dh_g, dh_privateBob);
    int dh_sharedSecretAlice = diffie_hellman(dh_p, dh_publicBob, dh_privateAlice);
    int dh_sharedSecretBob = diffie_hellman(dh_p, dh_publicAlice, dh_privateBob);

    std::cout << "Diffie-Hellman:" << std::endl;
    std::cout << "Shared Secret (Alice): " << dh_sharedSecretAlice << std::endl;
    std::cout << "Shared Secret (Bob): " << dh_sharedSecretBob << std::endl;

    // Эль-Гамаля
    int elgamal_p = 39;
    int elgamal_g = 4;
    int elgamal_x = 25;
    ElGamalKeys elgamalKeys = generateElGamalKeys(elgamal_p, elgamal_g, elgamal_x);
    int plaintext_elgamal = 18;
    ElGamalCiphertext elgamalCiphertext = elgamal_encrypt(plaintext_elgamal, elgamalKeys.p, elgamalKeys.g, elgamalKeys.y);
    int elgamalDecrypted = elgamal_decrypt(elgamalCiphertext, elgamalKeys.p, elgamalKeys.x);

    std::cout << "ElGamal:" << std::endl;
    std::cout << "Original: " << plaintext_elgamal << std::endl;
    std::cout << "Decrypted: " << elgamalDecrypted << std::endl;

    return 0;
}
