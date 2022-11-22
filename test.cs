using System;

namespace UserData
{
	class User : Level
	{
		private String	_userName;
		private Level	_LevelInfo;

		/*	Constructor for adding new user.	*/
		public User( String name ) {
			_userName = name;
			_LevelInfo = new Level();
		}

		/*	Constructor for updating from database. */
		public User( String name, int base_exp, int curr_exp, int curr_lvl, int multiplier ) {
			_userName = name;
			_LevelInfo = new Level(base_exp, curr_exp, curr_lvl, multiplier);
		}

		/*	Constructor for adding existing level data to user. */
		public User( String name, Level lvl ) {
			_userName = name;
			_LevelInfo = new Level(lvl);
		}

		/* Prints User details for testing.	*/
		public void printUser( ) {
			Console.WriteLine("*******************************");
			Console.WriteLine("User Name: " + _userName);
			Console.WriteLine("Exp to next level: " + _LevelInfo.getBaseExp());
			Console.WriteLine("Current exp: " + _LevelInfo.getCurrExp());
			Console.WriteLine("Current level: " + _LevelInfo.getCurrLvl());
			Console.WriteLine("Current score multiplier: " + _LevelInfo.getMultiplier());
			Console.WriteLine("*******************************");
		}
	}

	class Level
	{
		protected int	_base_exp;
		protected int	_curr_exp;
		protected int	_curr_lvl;
		protected int	_multiplier;

		/* Default constructor for new level data. To be used with new User. */
		public Level( ) {
			_base_exp = 100;
			_curr_exp = 0;
			_curr_lvl = 0;
			_multiplier = 1;
		}

		/* Constructor to update existing level data from database. */
		public Level( int base_exp, int curr_exp, int curr_lvl, int multiplier ) {
			_base_exp = base_exp;
			_curr_exp = curr_exp;
			_curr_lvl = curr_lvl;
			_multiplier = multiplier;
		}
	
		/* Copy Constructor. */
		public Level( Level cpy ) {
			_base_exp = cpy._base_exp;
			_curr_exp = cpy._curr_exp;
			_curr_lvl = cpy._curr_lvl;
			_multiplier = cpy._multiplier;
		}

		/*	Updates user exp. If this goes over the level cap, level goes up and
			threshold increased. */

		public void updateExp( int exp ) {
			_curr_exp += exp * _multiplier;
			if (_curr_exp >= _base_exp)
			{
				_curr_lvl++;
				updateBaseExp();
			}
		}

		/* Updates base exp by 150% when user levels up. */
		public void updateBaseExp( ) {
			_base_exp += (int)(_base_exp * 1.5);
		}

		/*	Setters	*/
		public void setMultiplier( int mult ) {
			_multiplier = mult;
		}

		/*	Getters */
		public int	getBaseExp( ) {
			return (_base_exp);
		}

		public int	getCurrExp( ) {
			return (_curr_exp);
		}

		public int	getCurrLvl( ) {
			return (_curr_exp);
		}

		public int	getMultiplier( ) {
			return (_multiplier);
		}
		
		/* Operator overload to add new experience points.	*/
		public static Level operator + ( Level curr, int exp ) {
			curr.updateExp(exp);
			return (curr);
		}

		/* Operator overload to modify score multiplier.	*/
		public static Level operator * ( Level curr, int mult ) {
			curr._multiplier = mult;
			return (curr);
		}
	
		/* Prints Level details for testing.	*/
		public void printLevel( ) {
			Console.WriteLine("*******************************");
			Console.WriteLine("Exp to next level: " + _base_exp);
			Console.WriteLine("Current exp: " + _curr_exp);
			Console.WriteLine("Current level: " + _curr_lvl);
			Console.WriteLine("Current score multiplier: " + _multiplier);
			Console.WriteLine("*******************************");
		}
		static void Main(string[] args)
		{
			Level test = new Level();
			test = test + 150;
			test = test * 2;
			test = test + 1;
			User tUser = new User("test", test);
			tUser.printUser();
		}
	}
}