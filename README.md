# The Supermarket Receipt Refactoring Kata 

The supermarket has a catalog with different types of products (rice, apples, milk, toothbrushes,...). Each product has a price, and the total price of the shopping cart is the total of all the prices of the items. You get a receipt that details the items you've bought, the total price and any discounts that were applied.

The supermarket runs special deals, e.g.
 - Buy two toothbrushes, get one free. Normal toothbrush price is €0.99
 - 20% discount on apples, normal price €1.99 per kilo.
 - 10% discount on rice, normal price €2.49 per bag
 - Five tubes of toothpaste for €7.49, normal price €1.79
 - Two boxes of cherry tomatoes for €0.99, normal price €0.69 per box.

These are just examples: the actual special deals change each week.

Create some test cases and aim to get good enough code coverage that you feel confident to do some refactoring.

When you have good test cases, identify code smells such as Long Method, Feature Envy. Apply relevant refactorings.

When you're confident you can handle this code, implement the new feature described below

## Start with the refactoring

If you would like to just do the refactoring part of this exercise, you can instead check out the 'with_tests' branch. 
Those tests have reasonably good coverage and should support most kinds of refactorings you'd like to do.
