using UsoDeElevadores;

class Program
{
    static void Main()
    {
        var service = new ElevadorService("input.json");

        Console.WriteLine("Andares menos utilizados: " + string.Join(", ", service.andarMenosUtilizado()));
        Console.WriteLine("Elevador mais frequentado: " + string.Join(", ", service.elevadorMaisFrequentado()));
        Console.WriteLine("Período de maior fluxo (elevador mais frequentado): " + string.Join(", ", service.periodoMaiorFluxoElevadorMaisFrequentado()));
        Console.WriteLine("Elevador menos frequentado: " + string.Join(", ", service.elevadorMenosFrequentado()));
        Console.WriteLine("Período de menor fluxo (elevador menos frequentado): " + string.Join(", ", service.periodoMenorFluxoElevadorMenosFrequentado()));
        Console.WriteLine("Período de maior uso conjunto: " + string.Join(", ", service.periodoMaiorUtilizacaoConjuntoElevadores()));
        Console.WriteLine($"Uso A: {service.percentualDeUsoElevadorA()}%");
        Console.WriteLine($"Uso B: {service.percentualDeUsoElevadorB()}%");
        Console.WriteLine($"Uso C: {service.percentualDeUsoElevadorC()}%");
        Console.WriteLine($"Uso D: {service.percentualDeUsoElevadorD()}%");
        Console.WriteLine($"Uso E: {service.percentualDeUsoElevadorE()}%");
    }
}
