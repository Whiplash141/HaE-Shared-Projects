﻿using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRageMath;

namespace IngameScript
{
	partial class Program
	{
        public class Scheduler
	    {
            public int runsPerTick;

            Queue<Task> Queue = new Queue<Task>();

            public Scheduler(int runsPerTick = 1)
            {
                this.runsPerTick = runsPerTick;
            }

            public void Main()
            {
                for (int i = 0; i < runsPerTick; i++)
                {
                    if (Queue.Count == 0)
                        return;

                    if (!Queue.First().MoveNext())
                        Queue.Dequeue().Dispose();
                }
            }

            public void AddTask(Task enumerator)
            {
                Queue.Enqueue(enumerator);
            }

            public void AddTask(IEnumerator<bool> enumerator)
            {
                var task = new Task(enumerator);
                AddTask(task);
            }
        }
	}
}
