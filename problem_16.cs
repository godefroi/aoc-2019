using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc_2019
{
	internal static class problem_16
	{
		private static string m_input = @"59728839950345262750652573835965979939888018102191625099946787791682326347549309844135638586166731548034760365897189592233753445638181247676324660686068855684292956604998590827637221627543512414238407861211421936232231340691500214827820904991045564597324533808990098343557895760522104140762068572528148690396033860391137697751034053950225418906057288850192115676834742394553585487838826710005579833289943702498162546384263561449255093278108677331969126402467573596116021040898708023407842928838817237736084235431065576909382323833184591099600309974914741618495832080930442596854495321267401706790270027803358798899922938307821234896434934824289476011";

		public static void Part1()
		{
			var num = m_input.ToCharArray().Select(c => Convert.ToInt32(c.ToString())).ToArray();

			for( var i = 0; i < 100; i++ )
				num = CalculateFFT(num);

			for( var i = 0; i < 8; i++ )
				Console.Write(num[i]);

			Console.WriteLine();
		}

		public static void Part2()
		{
			var inp = m_input.ToCharArray().Select(c => Convert.ToInt32(c.ToString())).ToArray();
			var num = new int[inp.Length * 10000];

			for( var i = 0; i < 10000; i++ )
				Array.Copy(inp, 0, num, i * inp.Length, inp.Length);

			for( var i = 0; i < 100; i++ )
				num = CalculateFFT(num);

			var offset = Convert.ToInt32(string.Join("", num.Take(7)));

			for( var i = 0; i < 8; i++ )
				Console.Write(num[offset + i]);

			Console.WriteLine();
		}

		private static int[] CalculateFFT(int[] input)
		{
			var ret = new List<int>();

			for( var i = 0; i < input.Length; i++ ) {
				var pg  = new PatternGenerator(new[] { 0, 1, 0, -1 }, i + 1);
				var sum = 0;

				for( var j = 0; j < input.Length; j++ )
					sum += input[j] * pg.Next();

				ret.Add(Convert.ToInt32(sum.ToString().Last().ToString()));
			}

			return ret.ToArray();
		}

		internal class PatternGenerator
		{
			private int[] m_pattern;
			private int   m_pos;
			private int   m_mult;
			private int   m_cmult;

			public PatternGenerator(int[] pattern, int multiplier)
			{
				m_pattern = pattern;
				m_mult    = multiplier;

				Reset();
			}

			public void Reset()
			{
				m_pos   = 0;
				m_cmult = 0;
			}

			public int Next()
			{
				if( ++m_cmult >= m_mult ) {
					m_pos++;
					m_cmult = 0;
				}

				return m_pattern[m_pos % m_pattern.Length];
			}
		}
	}
}
