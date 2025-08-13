using System;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Net.Http.Headers;
class Program
{
    // Entrada -> CPF | Processamento -> Algoritmo do Dígito Verificaor | Saída -> Validação do Dígito Verificador

    static void LerCpf(out string cpf)
    {
        Console.WriteLine("Informe o seu CPF no formato | (123.456.789-01)");
        do
        {
            Console.Write("-> ");
            cpf = Console.ReadLine();
            if (cpf.Length != 14) Console.WriteLine("Formato Inválido! Tente novamente.");
        } while (cpf.Length != 14);
    }
    static string FormataCpf(string cpf) => cpf.Trim().Replace(".", "").Replace("-", "");
    static int RetornaDigito(string cpf, int digitoParaVerificacao = 1)
    {
        int[] cpfInteiros = cpf.Select(x => (int)char.GetNumericValue(x)).ToArray();
        var multiplo = 0;
        var soma = 0;
        var index = 0;

        if (digitoParaVerificacao == 1) { multiplo = 10; index = multiplo; }
        else if (digitoParaVerificacao == 2) { multiplo = 11; index = multiplo; }

        for (int i = 0; i < index - 1; i++)
        {
            cpfInteiros[i] *= multiplo;
            soma += cpfInteiros[i];
            multiplo--;
        }

        var restoDivisao = soma % 11;

        if (restoDivisao >= 2) return 11 - restoDivisao;
        else return 0;
    }
    static (int, int) PreencheDigitosEncontrados(string cpfFormatado) => (RetornaDigito(cpfFormatado, 1), RetornaDigito(cpfFormatado, 2));
    static bool VerificaDigitos(int d1, int d2, string cpf) => (d1 == (int)char.GetNumericValue(cpf[9]) && d2 == (int)char.GetNumericValue(cpf[10]));
    static void ExibeResultado(int d1, int d2, string cpfFormatado) => Console.WriteLine(VerificaDigitos(d1, d2, cpfFormatado) ? "CPF válido" : "CPF Inválido");


    static void ExibicaoProgramaCpf()
    {
        LerCpf(out string cpf);
        string cpfFormatado = FormataCpf(cpf);
        (int d1, int d2) = PreencheDigitosEncontrados(cpfFormatado);
        ExibeResultado(d1, d2, cpfFormatado);
    }
    static void Main()
    {
        ExibicaoProgramaCpf();
    }
}