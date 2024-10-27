//#include <iostream>
//
//// Функция для нахождения НОД(a, b) и целых чисел u, v, удовлетворяющих соотношению Безу
//int extendedEuclidean(int a, int b, int& u, int& v) {
//    // Базовый случай: если b равно 0, НОД(a, b) равен a, и u, v равны соответственно 1 и 0
//    if (b == 0) {
//        u = 1;
//        v = 0;
//        return a;
//    }
//
//    // Рекурсивный шаг: применяем расширенный алгоритм Евклида для b и остатка от деления a на b
//    int u1, v1;
//    int gcd = extendedEuclidean(b, a % b, u1, v1);
//
//    // Обновляем значения u и v на основе рекурсивного вызова
//    u = v1;
//    v = u1 - (a / b) * v1;
//
//    return gcd;
//}
//
//int main() {
//    setlocale(LC_ALL, "Russian");
//    int a = 83748733;
//    int b = 73435591;
//    int u, v;
//
//    int gcd = extendedEuclidean(a, b, u, v);
//
//    std::cout << "НОД(" << a << ", " << b << ") = " << gcd << std::endl;
//    std::cout << "u = " << u << ", v = " << v << std::endl;
//    std::cout << a << " * " << u << " + " << b << " * " << v << " = " << gcd << std::endl;
//
//    return 0;
//}








#include <iostream>

// функция для вычисления остатка от деления числа в степени на другое число
int moduloexponentiation(int base, int exponent, int divisor) {
    if (exponent == 0) {
        return 1;
    }

    int result = 1;
    base = base % divisor; // берем остаток от деления базы на делитель

    while (exponent > 0) {
        if (exponent % 2 == 1) {
            result = (result * base) % divisor; // если текущий бит степени равен 1, умножаем результат на базу и берем остаток
        }

        exponent = exponent >> 1; // сдвигаем биты степени вправо
        base = (base * base) % divisor; // возведение базы в квадрат и взятие остатка
    }

    return result;
}

int main() {
    setlocale(LC_ALL, "russian"); 
    int base = 1996;
    int exponent = 2003;
    int divisor = 11;

    int result = moduloexponentiation(base, exponent, divisor);

    std::cout << "остаток от деления " << base << " в степени " << exponent << " на " << divisor << " равен: " << result << std::endl;

    return 0;
}