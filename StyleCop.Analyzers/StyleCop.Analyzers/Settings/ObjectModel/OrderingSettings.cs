﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Settings.ObjectModel
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using LightJson;

    internal class OrderingSettings
    {
        private static readonly ImmutableArray<OrderingTrait> DefaultElementOrder =
            ImmutableArray.Create(
                OrderingTrait.Kind,
                OrderingTrait.Accessibility,
                OrderingTrait.Constant,
                OrderingTrait.Static,
                OrderingTrait.Readonly);

        /// <summary>
        /// This is the backing field for the <see cref="ElementOrder"/> property.
        /// </summary>
        private ImmutableArray<OrderingTrait>.Builder elementOrder;

        /// <summary>
        /// This is the backing field for the <see cref="SystemUsingDirectivesFirst"/> property.
        /// </summary>
        private bool systemUsingDirectivesFirst;

        /// <summary>
        /// This is the backing field for the <see cref="UsingDirectivesPlacement"/> property.
        /// </summary>
        private UsingDirectivesPlacement usingDirectivesPlacement;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderingSettings"/> class during JSON deserialization.
        /// </summary>
        protected internal OrderingSettings()
        {
            this.elementOrder = ImmutableArray.CreateBuilder<OrderingTrait>();
            this.systemUsingDirectivesFirst = true;
            this.usingDirectivesPlacement = UsingDirectivesPlacement.InsideNamespace;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderingSettings"/> class.
        /// </summary>
        /// <param name="orderingSettingsObject">The JSON object containing the settings.</param>
        protected internal OrderingSettings(JsonObject orderingSettingsObject)
            : this()
        {
            foreach (var kvp in orderingSettingsObject)
            {
                switch (kvp.Key)
                {
                case "elementOrder":
                    kvp.AssertIsArray();
                    foreach (var value in kvp.Value.AsJsonArray)
                    {
                        this.elementOrder.Add(value.ToEnumValue<OrderingTrait>(kvp.Key));
                    }

                    break;

                case "systemUsingDirectivesFirst":
                    this.systemUsingDirectivesFirst = kvp.ToBooleanValue();
                    break;

                case "usingDirectivesPlacement":
                    this.usingDirectivesPlacement = kvp.ToEnumValue<UsingDirectivesPlacement>();
                    break;

                default:
                    break;
                }
            }
        }

        public ImmutableArray<OrderingTrait> ElementOrder
        {
            get
            {
                return this.elementOrder.Count > 0 ? this.elementOrder.ToImmutable() : DefaultElementOrder;
            }
        }

        public bool SystemUsingDirectivesFirst =>
            this.systemUsingDirectivesFirst;

        public UsingDirectivesPlacement UsingDirectivesPlacement =>
            this.usingDirectivesPlacement;
    }
}
