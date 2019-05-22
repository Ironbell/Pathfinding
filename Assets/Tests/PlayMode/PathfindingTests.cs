using System.Collections;
using Pathfinding.Algorithm;
using Pathfinding.Graph;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Pathfinding.Tests.PlayMode
{
    public class PathfindingTests
    {
        [UnityTest]
        public IEnumerator GivenLevelWithoutObstacles_WhenRunningDijkstra_ThenGoalFound()
        {
            Pathfinder.TypeOverride = PathfindingType.Dijkstra;
            GraphBoard.ObstacleProbability = 0;
            var asyncLoad = SceneManager.LoadSceneAsync("Level1");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            var pathfinder = (Pathfinder)Object.FindObjectOfType(typeof(Pathfinder));

            while (!pathfinder.IsComplete)
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator GivenLevelWithoutObstacles_WhenRunningAStar_ThenGoalFound()
        {
            Pathfinder.TypeOverride = PathfindingType.AStar;
            GraphBoard.ObstacleProbability = 0;
            var asyncLoad = SceneManager.LoadSceneAsync("Level1");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            var pathfinder = (Pathfinder)Object.FindObjectOfType(typeof(Pathfinder));

            while (!pathfinder.IsComplete)
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator GivenLevelWithObstacles_WhenRunningDijkstra_ThenGoalFound()
        {
            Pathfinder.TypeOverride = PathfindingType.Dijkstra;
            GraphBoard.ObstacleProbability = 0.2f;
            var asyncLoad = SceneManager.LoadSceneAsync("Level2");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            var pathfinder = (Pathfinder)Object.FindObjectOfType(typeof(Pathfinder));

            while (!pathfinder.IsComplete)
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator GivenLevelWithObstacles_WhenRunningAStar_ThenGoalFound()
        {
            GraphBoard.ObstacleProbability = 0.2f;
            Pathfinder.TypeOverride = PathfindingType.AStar;
            var asyncLoad = SceneManager.LoadSceneAsync("Level2");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            var pathfinder = (Pathfinder)Object.FindObjectOfType(typeof(Pathfinder));

            while (!pathfinder.IsComplete)
            {
                yield return null;
            }
        }
    }
}
