using System.Drawing;

namespace labirint
{
    class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(120, 30);
            Point shiftMapDraw = new Point(20, 0);

            while (true)
            {
                Labyrinth labyrinth = new Labyrinth(80, 20, 100);
                int startCoins = labyrinth.Coin;
                int coin = 0;

                Player player = new Player(labyrinth, labyrinth.EntranceDoor);
                EnemyManager enemyManager = new EnemyManager(labyrinth, player, 2, 2, 2);
                PlayerMovement playerMovement = new PlayerMovement(player, labyrinth);
                PlayerAttacks playerAttacks = new PlayerAttacks(player, enemyManager, labyrinth, shiftMapDraw);
                Input input = new Input();
                input.AddInput(playerMovement.Movement);
                input.AddInput(playerAttacks.Attacks);

                long lastMoveEnemy = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long lastRespawanEnemy = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                labyrinth.Draw(20, 0);

                Console.SetCursorPosition(0, 25);
                Console.WriteLine("Q удар ногой | 10 energy");
                Console.WriteLine("G выстрел    | 25 energy");
                Console.WriteLine();
                Console.WriteLine("Enter устоновить бомбу | 49 energy");
                Console.Write("Space взорвать бомбу   | 1 energy");

                while (true)
                {
                    if (player.Health <= 0 || player.Energy <= 0 || enemyManager.CoinCollected >= startCoins / 2)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(50, 15);
                        Console.WriteLine("You are dead");
                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    }

                    if (labyrinth.Coin == 0 || enemyManager.CountEnemy == 0)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(55, 15);
                        Console.WriteLine("You win!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    }

                    if (lastRespawanEnemy + 30000 < DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond)
                    {
                        int enemyType = new Random().Next(0, 3);
                        if (enemyType == 0)
                        {
                            enemyManager.AddPsychAtDisco();
                        }
                        else if (enemyType == 1)
                        {
                            enemyManager.AddSafariHunter();
                        }
                        else if (enemyType == 2)
                        {
                            enemyManager.AddSafariHunter();
                        }
                        lastRespawanEnemy = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    }

                    input.GetInput();

                    if (lastMoveEnemy + 300 < DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond)
                    {
                        lastMoveEnemy = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                        enemyManager.Move();
                    }


                    Enemy enemy = enemyManager.CheckCollisionEnemies(shiftMapDraw);
                    if (enemy != null)
                    {
                        player.Hit(enemy.OnHitHealth);
                        Console.SetCursorPosition(enemy.Coordinates.X + shiftMapDraw.X, enemy.Coordinates.Y + shiftMapDraw.Y);
                        Console.Write(labyrinth.Map[enemy.Coordinates.Y, enemy.Coordinates.X]);
                    }

                    if (labyrinth.ColletCoin(player.Coordinates))
                    {
                        coin++;
                    }
                    if (labyrinth.ColletCoffe(player.Coordinates))
                    {
                        player.Energy += 25;
                    }
                    if (labyrinth.ColletHeart(player.Coordinates))
                    {
                        player.Heal(5);
                    }

                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Coin: " + coin + "     ");
                    Console.WriteLine("Coin to win: " + labyrinth.Coin + "  ");
                    Console.WriteLine("Enemy: " + enemyManager.CountEnemy + "     ");
                    Console.WriteLine();
                    Console.WriteLine("Player stats");
                    Console.WriteLine("Energy: " + player.Energy + "     ");
                    Console.WriteLine("Health: " + player.Health + "%    ");

                    player.Draw(shiftMapDraw);
                    enemyManager.Draw(shiftMapDraw);
                }
            }
        }
    }
}
