using System.Diagnostics.CodeAnalysis;

namespace Cortex.Collections.Generic
{
    public class ConcurrentPriorityQueue<TElement, TPriority> : PriorityQueue<TElement, TPriority>
    {
        private readonly object _sync = new object();


        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.PriorityQueue`2 class as thread-safe
        /// </summary>
        public ConcurrentPriorityQueue() : base()
        {

        }

        /// <summary>
        ///Initializes a new instance of the System.Collections.Generic.PriorityQueue`2 class with the specified custom priority comparer.
        /// </summary>
        /// <param name="comparer">Custom comparer dictating the ordering of elements. Uses System.Collections.Generic.Comparer`1. Default if the argument is null.</param>
        public ConcurrentPriorityQueue(IComparer<TPriority>? comparer) : base(comparer)
        {

        }

        public ConcurrentPriorityQueue(IEnumerable<(TElement Element, TPriority Priority)> items) : base(items)
        {

        }

        public ConcurrentPriorityQueue(IEnumerable<(TElement Element, TPriority Priority)> items, IComparer<TPriority>? comparer) : base(items, comparer)
        {

        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.PriorityQueue`2 class with the specified initial capacity and custom priority comparer.
        /// </summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException:The specified initialCapacity was negative."
        /// <param name="initialCapacity">Initial capacity to allocate in the underlying heap array.</param>
        public ConcurrentPriorityQueue(int initialCapacity) : base(initialCapacity)
        {

        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.PriorityQueue`2 class with the specified initial capacity and custom priority comparer.
        /// </summary>
        /// <exception cref="T:System.ArgumentOutOfRangeException:The specified initialCapacity was negative."
        /// <param name="initialCapacity">Initial capacity to allocate in the underlying heap array.</param>
        /// <param name="comparer">Custom comparer dictating the ordering of elements. Uses System.Collections.Generic.Comparer`1. Default if the argument is null</param>
        public ConcurrentPriorityQueue(int initialCapacity, IComparer<TPriority>? comparer) : base(initialCapacity, comparer)
        {

        }

        /// <summary>
        /// Adds the specified element with associated priority to the System.Collections.Generic.PriorityQueue`2.
        /// </summary>
        /// <param name="element">The element to add to the System.Collections.Generic.PriorityQueue`2.</param>
        /// <param name="priority">The priority with which to associate the new element.</param>
        /// <returns>true if the element is sucessfully added; false if the System.Collections.Generic.PriorityQueue is used by another thread.</returns>
        public bool TryEnqueue(TElement element, TPriority priority)
        {
            lock (_sync)
            {
                try
                {
                    base.Enqueue(element, priority);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Adds the specified element with associated priority to the System.Collections.Generic.PriorityQueue`2,
        /// and immediately removes the minimal element, returning the result.
        /// </summary>
        /// <param name="element">The element to add to the System.Collections.Generic.PriorityQueue`2.</param>
        /// <param name="priority">The priority with which to associate the new element.</param>
        /// <param name="dequeuedElement">The removed element after adding the element</param>
        /// <returns>true if the element is successfully added and removed; false if the System.Collections.Generic.PriorityQueue`2 is empty or was in used by another thread.</returns>
        public bool TryEnqueueDequeue(TElement element, TPriority priority, out TElement? dequeuedElement)
        {
            lock (_sync)
            {
                dequeuedElement = default;
                try
                {
                    dequeuedElement = base.EnqueueDequeue(element, priority);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Trys to remove the minimal element from the System.Collections.Generic.PriorityQueue`2,
        /// and copies it to the element parameter, and its associated priority to the priority
        /// parameter.
        /// </summary>
        /// <param name="dequeuedElement">The removed element.</param>
        /// <param name="dequeuedPriority">The priority associated with the removed element.</param>
        /// <returns>true if the element is successfully removed; false if the System.Collections.Generic.PriorityQueue`2 is empty or was in used by another thread.</returns>
        public new bool TryDequeue([MaybeNullWhen(false)] out TElement? dequeuedElement, [MaybeNullWhen(false)] out TPriority? dequeuedPriority)
        {
            lock (_sync)
            {
                dequeuedElement = default;
                dequeuedPriority = default;

                try
                {
                    return base.TryDequeue(out dequeuedElement, out dequeuedPriority);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns a value that indicates whether there is a minimal element in the System.Collections.Generic.PriorityQueue`2,
        /// and if one is present, copies it to the element parameter, and its associated
        /// priority to the priority parameter. The element is not removed from the System.Collections.Generic.PriorityQueue`2.
        /// </summary>
        /// <param name="dequeuedElement">The minimal element in the queue.</param>
        /// <param name="dequeuedPriority">The priority associated with the minimal element.</param>
        /// <returns>true if there is a minimal element; false if the System.Collections.Generic.PriorityQueue`2 is empty.</returns>
        public new bool TryPeek([MaybeNullWhen(false)] out TElement? dequeuedElement, [MaybeNullWhen(false)] out TPriority dequeuedPriority)
        {
            lock (_sync)
            {
                dequeuedElement = default;
                dequeuedPriority = default;

                try
                {
                    return base.TryPeek(out dequeuedElement, out dequeuedPriority);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Enqueues a sequence of elements pairs to the System.Collections.Generic.PriorityQueue`2 foreach element with priority.
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException:The specified elements argument was null."
        /// <param name="items">The elements with priority to add to the queue</param>
        /// <returns>true if the element is sucessfully added; false if the System.Collections.Generic.PriorityQueue is used by another thread.</returns>
        public bool TryEnqueueRange(IEnumerable<(TElement Element, TPriority Priority)> items)
        {
            lock (_sync)
            {
                try
                {
                    base.EnqueueRange(items);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Enqueues a sequence of elements pairs to the System.Collections.Generic.PriorityQueue`2 all associated with the specified priority.
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException:The specified elements argument was null."
        /// <param name="elements">The elements with priority to add to the queue.</param>
        /// <param name="priority">The priority to associate with the new elements.</param>
        /// <returns>true if the element is sucessfully added; false if the System.Collections.Generic.PriorityQueue is used by another thread.</returns>
        public bool TryEnqueueRange(IEnumerable<TElement> elements, TPriority priority)
        {
            lock (_sync)
            {
                try
                {
                    base.EnqueueRange(elements, priority);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
