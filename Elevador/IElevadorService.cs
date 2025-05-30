using System.Text.Json;

namespace UsoDeElevadores
{
    public interface IElevadorService
    {
        List<int> andarMenosUtilizado();
        List<char> elevadorMaisFrequentado();
        List<char> periodoMaiorFluxoElevadorMaisFrequentado();
        List<char> elevadorMenosFrequentado();
        List<char> periodoMenorFluxoElevadorMenosFrequentado();
        List<char> periodoMaiorUtilizacaoConjuntoElevadores();
        float percentualDeUsoElevadorA();
        float percentualDeUsoElevadorB();
        float percentualDeUsoElevadorC();
        float percentualDeUsoElevadorD();
        float percentualDeUsoElevadorE();
    }

    public class Entrada
    {
        public int andar { get; set; }
        public char elevador { get; set; }
        public char turno { get; set; }
    }

    public class ElevadorService : IElevadorService
    {
        private readonly List<Entrada> entradas;

        public ElevadorService(string caminhoJson)
        {
            var json = File.ReadAllText(caminhoJson);
            entradas = JsonSerializer.Deserialize<List<Entrada>>(json) ?? new List<Entrada>();
        }

        public List<int> andarMenosUtilizado()
        {
            var grupos = entradas.GroupBy(e => e.andar)
                                 .Select(g => new { Andar = g.Key, Total = g.Count() });

            int min = grupos.Min(a => a.Total);
            return grupos.Where(a => a.Total == min).Select(a => a.Andar).ToList();
        }

        public List<char> elevadorMaisFrequentado()
        {
            var grupos = entradas.GroupBy(e => e.elevador)
                                 .Select(g => new { Elevador = g.Key, Total = g.Count() });

            int max = grupos.Max(e => e.Total);
            return grupos.Where(e => e.Total == max).Select(e => e.Elevador).ToList();
        }

        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            var maisFreq = elevadorMaisFrequentado();
            return maisFreq.SelectMany(elev =>
                        entradas.Where(e => e.elevador == elev)
                                .GroupBy(e => e.turno)
                                .OrderByDescending(g => g.Count())
                                .Take(1)
                                .Select(g => g.Key))
                    .ToList();
        }

        public List<char> elevadorMenosFrequentado()
        {
            var grupos = entradas.GroupBy(e => e.elevador)
                                 .Select(g => new { Elevador = g.Key, Total = g.Count() });

            int min = grupos.Min(e => e.Total);
            return grupos.Where(e => e.Total == min).Select(e => e.Elevador).ToList();
        }

        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            var menosFreq = elevadorMenosFrequentado();
            return menosFreq.SelectMany(elev =>
                        entradas.Where(e => e.elevador == elev)
                                .GroupBy(e => e.turno)
                                .OrderBy(g => g.Count())
                                .Take(1)
                                .Select(g => g.Key))
                    .ToList();
        }

        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            var grupos = entradas.GroupBy(e => e.turno)
                                 .Select(g => new { Turno = g.Key, Total = g.Count() });

            int max = grupos.Max(t => t.Total);
            return grupos.Where(t => t.Total == max).Select(t => t.Turno).ToList();
        }

        private float CalcularPercentual(char elevador)
        {
            int total = entradas.Count;
            int uso = entradas.Count(e => e.elevador == elevador);
            return (float)Math.Round((double)uso * 100 / total, 2);
        }

        public float percentualDeUsoElevadorA() => CalcularPercentual('A');
        public float percentualDeUsoElevadorB() => CalcularPercentual('B');
        public float percentualDeUsoElevadorC() => CalcularPercentual('C');
        public float percentualDeUsoElevadorD() => CalcularPercentual('D');
        public float percentualDeUsoElevadorE() => CalcularPercentual('E');
    }
}
