using System;
using System.Threading;
using Epic.Core;

namespace Epic.Engine.GameFramework
{
	public class FTickFunction
	{
		public ETickingGroup tickGroup = default;
		public ETickingGroup endTickGroup = default;

		public sbyte tickEvenWhenPaused = 1;
		public sbyte canEverTick = 1;
		public sbyte startWithTickEnabled = 1;
		public sbyte allowTickOnDedicatedServer = 1;
		public sbyte highPriority = 1;
		public sbyte runOnAnyThread = 1;

		enum ETickState : sbyte
		{
			Disabled,
			Enabled,
			CoolingDown,
		}

		ETickState tickState = default;
		public float tickInterval = default;
		FTickPrerequisite[] prerequisites = default;

		class FInternalData
		{
			/** Whether the tick function is registered. */
			public bool bRegistered = true;

			/** Cache whether this function was rescheduled as an interval function during StartParallel */
			public bool bWasInterval = true;

			/** Internal data that indicates the tick group we actually started in (it may have been delayed due to prerequisites) **/
			public ETickingGroup ActualStartTickGroup;

			/** Internal data that indicates the tick group we actually started in (it may have been delayed due to prerequisites) **/
			public ETickingGroup ActualEndTickGroup;

			/** Internal data to track if we have started visiting this tick function yet this frame **/
			public int TickVisitedGFrameCounter;

			/** Internal data to track if we have finshed visiting this tick function yet this frame **/
			public int TickQueuedGFrameCounter;

			/** Pointer to the task, only used during setup. This is often stale. **/
			public Action TaskPointer;

			/** The next function in the cooling down list for ticks with an interval*/
			public FTickFunction Next;

			/** 
		 * If TickFrequency is greater than 0 and tick state is CoolingDown, this is the time, 
		 * relative to the element ahead of it in the cooling down list, remaining until the next time this function will tick 
		 **/
			public float RelativeTickCooldown;

			/** 
		 * The last world game time at which we were ticked. Game time used is dependent on bTickEvenWhenPaused
		 * Valid only if we've been ticked at least once since having a tick interval; otherwise set to -1.f
		 **/
			public float LastTickGameTimeSeconds;

			/** Back pointer to the FTickTaskLevel containing this tick function if it is registered **/
			public FTickTaskLevel TickTaskLevel;
		};

		/** Lazily allocated struct that contains the necessary data for a tick function that is registered. **/
		static FInternalData internalData;

		protected FTickFunction()
		{
		}

		~FTickFunction()
		{
		}

		public void RegisterTickFunction(ULevel level)
		{
		}

		public void UnregisterTickFunction()
		{
			throw new NotImplementedException();
		}

		public bool IsTickFunctionRegistered()
		{
			return internalData != null && internalData.bRegistered;
		}

		public void SetTickFunctionRegistered(bool isEnabled)
		{
			throw new NotImplementedException();
		}

		public bool IsTickFunctionEnabled()
		{
			return tickState != ETickState.Disabled;
		}

		public bool IsCompletionHandleValid()
		{
			return internalData != null && internalData.TaskPointer != null;
		}

		public ETickingGroup GetActualTickGroup()
		{
			return internalData != null ? internalData.ActualStartTickGroup : tickGroup;
		}

		public ETickingGroup GetActualEnTickGroup()
		{
			return internalData != null ? internalData.ActualEndTickGroup : endTickGroup;
		}

		public void AddPrerequisite(UnrealObject targetObject, FTickFunction targetTickFunction)
		{
			throw new NotImplementedException();
		}

		public void RemovePrerequisite(UnrealObject targetObject, FTickFunction targetTickFunction)
		{
			throw new NotImplementedException();
		}

		public void SetPriorityIncludingPrerequisites(bool inHighPriority)
		{
			throw new NotImplementedException();
		}

		public FTickPrerequisite[] GetPrerequisites()
		{
			return prerequisites;
		}

		public float GetLastTickGameTime()
		{
			return internalData != null ? internalData.LastTickGameTimeSeconds : -1.0f;
		}

		void QueueTickFunction(FTickTaskSequencer tts, FTickContext tickContext)
		{
			throw new NotImplementedException();
		}

		void QueueTickFunctionParallel(FTickContext tickContext, FTickFunction[] stackGorCycleDetection)
		{
			throw new NotImplementedException();
		}

		float CalculateDeltaTime(FTickContext tickContext)
		{
			throw new NotImplementedException();
		}

		void ShowPrerequisites(int indent = 1)
		{
			throw new NotImplementedException();
		}

		protected virtual void ExecuteTick(float deltaTime, ELevelTick tickType, Thread currentThread,
			FGraphEventRef myCompletionGraphEvent)
		{
			//check(0);
			throw new NotImplementedException();
		}

		protected virtual string DiagnosticMessage()
		{
			//check(0);
			return "invalid";
		}
	}

	public class FGraphEventRef
	{
	}

	class FTickContext
	{
		/** Delta time to tick **/
		float DeltaSeconds;

		/** Tick type **/
		ELevelTick TickType;

		/** Tick type **/
		ETickingGroup TickGroup;

		/** Current or desired thread **/
		Thread Thread;

		/** The world in which the object being ticked is contained. **/
		UWorld World;
	}

	public class UWorld
	{
		
	}

	class FTickTaskSequencer
	{
	}
}