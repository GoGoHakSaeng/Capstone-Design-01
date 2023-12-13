using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WorldMapStrategyKit.MapGenerator.Geom;

namespace WorldMapStrategyKit {

	public partial class MapCity {

		public int provinceIndex;
		public int countryIndex;
		public Vector2 unity2DLocation;
		public int population;
		public CITY_CLASS cityClass;

		public MapCity (int provinceIndex, int countryIndex, int population, Vector2 location, CITY_CLASS cityClass) {
			this.provinceIndex = provinceIndex;
			this.countryIndex = countryIndex;
			this.population = population;
			this.unity2DLocation = location;
			this.cityClass = cityClass;
		}

	}
}

